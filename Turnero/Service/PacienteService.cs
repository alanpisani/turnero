using Turnero.Dto;
using Turnero.Mappers;
using Turnero.Models;
using Turnero.Repositories.Interfaces;
using Turnero.Validators.PacienteValidators;

namespace Turnero.Service
{
	public class PacienteService(UsuarioService usuarioService, IUnitOfWork unit)
	{
		private readonly UsuarioService _usuarioService = usuarioService;
		private readonly IUnitOfWork _unitOfWork = unit;

		public async Task<ServiceResponse<Paciente>> RegistrarPaciente(PacienteDto dto)
		{
			UsuarioDto usuarioDto = UsuarioMapper.DtoHijosAUsuarioDto(dto); //Se crea al UsuarioDto necesario
			var usuario = _usuarioService.CrearUsuario(usuarioDto, 1); //Se crea un Usuario model en base al UsuarioDto, para la bd

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

				return new ServiceResponse<Paciente> //Se envia la respuesta exitosa para el Controller 
				{
					Exito = true,
					Mensaje = "Paciente creado con éxito", //💪🏻😎
					Cuerpo = paciente //El JSON del paciente, para el front
				};
			}
			catch(Exception e)
			{
				await _unitOfWork.RollbackAsync();

				return new ServiceResponse<Paciente> //Se envia la respuesta exitosa para el Controller 
				{
					Mensaje = $"Error inesperado. Intente registrarse más tarde: {e}"
				};
			}
		}

		public async Task<ServiceResponse<List<Paciente>>> MostrarTodosLosPacientes()
		{
			var pacientes = await _unitOfWork.Pacientes.ToListAsyncAllPacientes();

			if(pacientes.Count == 0)
			{
				return new ServiceResponse<List<Paciente>>
				{
					Mensaje = "No se encontraron pacientes registrados en el sistema"
				};
			}

			return new ServiceResponse<List<Paciente>>
			{
				Exito = true,
				Cuerpo = pacientes
			};
		}
	}
}
