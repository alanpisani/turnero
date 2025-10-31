using Humanizer;
using Turnero.Domain.PacienteDomain;
using Turnero.Dto;
using Turnero.Dto.Paciente;
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

		public async Task<Paciente> RegistrarPaciente(PacienteRequestDto dto)
		{
			UsuarioDto usuarioDto = UsuarioMapper.DtoHijosAUsuarioDto(dto); //Se crea al UsuarioDto necesario
			var usuario = _usuarioService.CrearUsuario(usuarioDto, 1); //Se crea un Usuario model en base al UsuarioDto, para la bd

			var result = await new CreatePacienteDomain(_unitOfWork).Validar(dto);

			if (!result.EsValido) throw new BussinessErrorContentException(result.Errores);
	
			await _unitOfWork.BeginTransactionAsync();

			try
			{
				await _unitOfWork.Usuarios.AddAsyncUsuario(usuario); //Se sube el Usuario model a la bd
				await _unitOfWork.CompleteAsync();

				Paciente paciente = PacienteMapper.DePacienteDtoAPaciente(dto, usuario.IdUsuario); //Se crea un Paciente model con el id del usuario recien creado (MAGIA DE EF. El modelo tiene el id)
				var coberturas = PacienteMapper.CrearCoberturasPaciente(dto, paciente.IdUsuario); //Se crean las coberturas model del paciente

				await _unitOfWork.Pacientes.AddPaciente(paciente); //Se sube al paciente a la bd
				await _unitOfWork.Pacientes.AddCoberturas(coberturas); //Se sube el registro de la/s tabla/s intermedia/s a la BD

				await _unitOfWork.CompleteAsync();
				await _unitOfWork.CommitAsync();

				return paciente;
			}
			catch (Exception e) {

				throw new Exception($"Hubo un error al registrar paciente. Inténtelo más tarde. Error: {e}");
			}


		}

		public async Task<ResponseDto<UsuarioRapidoDto>> RegistrarPacienteRapido(UsuarioRapidoDto dto)
		{
			//validacion simple:

			var usuarioInDb = await _unitOfWork.Usuarios.AnyUsuarioByDni(dto.Dni);

			if (usuarioInDb) throw new Exception("El dni ingresado ya se encuentra registrado en el sistema"); //TODO cambiar!!

			var usuario = _usuarioService.CrearUsuarioRapido(dto, 1); //Se crea un Usuario model en base al UsuarioRapidoDto, para la bd

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

		public async Task<List<PacienteResponseGet>> MostrarTodosLosPacientes()
		{
			var pacientes = await _unitOfWork.Pacientes.ToListAsyncAllPacientes();

			var pacientesDto = pacientes
				.Select(p => PacienteMapper.DePacienteAPacienteDtoGet(p))
				.ToList();

			return pacientesDto;
		}

		public async Task<PacienteResponseGet> MostrarPacientePorId(int idPaciente)
		{
			var paciente = await _unitOfWork.Pacientes.GetPacienteById(idPaciente);

			if (paciente == null) throw new NotFoundException($"No se encontró el paciente con ID {idPaciente}");

			var pacienteDtoGet = PacienteMapper.DePacienteAPacienteDtoGet(paciente);

			return pacienteDtoGet;
		}
	}
}
