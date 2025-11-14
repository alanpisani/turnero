using Turnero.Common.Enums;
using Turnero.Dto;
using Turnero.Dto.Paciente;
using Turnero.Dto.TurnoDto;
using Turnero.Dto.Usuario;
using Turnero.Exceptions;
using Turnero.Mappers;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Service
{
    public class PacienteService(UsuarioService usuarioService, IUnitOfWork unit)
	{
		private readonly UsuarioService _usuarioService = usuarioService;
		private readonly IUnitOfWork _unitOfWork = unit;

		public async Task<ResponseDto<PacienteYTurnoResponseDto>> RegistrarPacienteConTurno(PacienteConTurnoRequestDto dto)
		{

			var usuario = new Usuario
			{
				Nombre = dto.Nombre,
				Apellido = dto.Apellido,
				Dni=dto.Dni,
				Email=dto.Email,
				Password= dto.Contrasenia,
				Rol= RolesUsuario.Paciente.ToString()
			};

			usuario.Password = _usuarioService.Hashear(usuario, usuario.Password);

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				// Crear Usuario
				await _unitOfWork.Usuarios.AddAsyncUsuario(usuario);
				await _unitOfWork.CompleteAsync();

				//Crear Paciente
				var paciente = PacienteMapper.DePacienteDtoAPaciente(dto, usuario.IdUsuario);
				await _unitOfWork.Pacientes.AddPaciente(paciente);
				await _unitOfWork.CompleteAsync();

				// Crear Turno asociado
				var turnoDto = new TurnoRequestDto
				{
					IdProfesional = dto.Turno.IdProfesional,
					IdEspecialidad = dto.Turno.IdEspecialidad,
					Dia = dto.Turno.Dia,
					Hora = dto.Turno.Hora,
					IdPaciente = paciente.IdUsuario, // recién creado
				};

				var turnoService = new TurnoService(_unitOfWork);
				var turnoResponse = await turnoService.SolicitarTurno(turnoDto, true);

				if (turnoResponse != null) { 
				
				}

				await _unitOfWork.CommitAsync();

				// Devolver ambos resultados
				return new ResponseDto<PacienteYTurnoResponseDto>
				{
					Success = true,
					Message = "Paciente registrado y turno creado correctamente",
					Data = new PacienteYTurnoResponseDto
					{
						Paciente = PacienteMapper.ToDto(paciente),
						Turno = turnoResponse.Data!
					}
				};
			}
			catch (Exception e)
			{
				await _unitOfWork.RollbackAsync();
				if (e is BussinessErrorContentException)
					throw;
				throw new Exception($"Error al registrar paciente y turno. {e.Message}");
			}
		}

		public async Task<ResponseDto<UsuarioRapidoDto>> RegistrarPacienteRapido(UsuarioRapidoDto dto)
		{
			//validacion simple:

			var usuarioInDb = await _unitOfWork.Usuarios.AnyUsuarioByDni(dto.Dni);

			if (usuarioInDb) throw new Exception("El dni ingresado ya se encuentra registrado en el sistema"); //TODO cambiar!!

			var usuario = _usuarioService.CrearUsuarioRapido(dto, RolesUsuario.Paciente.ToString()); //Se crea un Usuario model en base al UsuarioRapidoDto, para la bd

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				await _unitOfWork.Usuarios.AddAsyncUsuario(usuario); //Se sube el Usuario model a la bd
				await _unitOfWork.CompleteAsync();

				Paciente paciente = new Paciente { IdUsuario= usuario.IdUsuario };
				await _unitOfWork.Pacientes.AddPaciente(paciente); //Se sube al paciente a la bd
				
				await _unitOfWork.CompleteAsync();
				await _unitOfWork.CommitAsync();

				return new ResponseDto<UsuarioRapidoDto> { 
					Success= true,
					Message= $"Usuario creado con éxito. Dni: {dto.Dni}"
				};
			}
			catch (Exception e)
			{

				throw new Exception($"Hubo un error al registrar paciente. Inténtelo más tarde. \nError: {e}");
			}
		}

		public async Task<ResponseDto<List<PacienteResponseDto>>> MostrarTodosLosPacientes()
		{
			var pacientes = await _unitOfWork.Pacientes.ToListAsyncAllPacientes();

			var pacientesDto = pacientes
				.Select(p => PacienteMapper.ToDto(p))
				.ToList();

			return new ResponseDto<List<PacienteResponseDto>> { 
				Success = true,
				Message = "Pacientes consultados con éxito",
				Data = pacientesDto
			};
		}

		public async Task<PacienteResponseDto> MostrarPacientePorId(int idPaciente)
		{
			var paciente = await _unitOfWork.Pacientes.GetPacienteById(idPaciente);

			if (paciente == null) throw new NotFoundException($"No se encontró el paciente con ID {idPaciente}");

			var pacienteDtoGet = PacienteMapper.ToDto(paciente);

			return pacienteDtoGet;
		}
	}
}
