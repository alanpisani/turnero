using Turnero.Common.Helpers;
using Turnero.Domain.PacienteDomain;
using Turnero.Domain.ProfesionalDomain;
using Turnero.Dto;
using Turnero.Mappers;
using Turnero.Models;
using Turnero.Repositories.Interfaces;
using Turnero.Validators.ProfesionalValidators;

namespace Turnero.Service
{
	public class ProfesionalService(UsuarioService service, IUnitOfWork unit)
	{ 
		private readonly UsuarioService _service = service;
		private readonly IUnitOfWork _unitOfWork = unit;

		public async Task<ServiceResponse<Profesional>> RegistrarProfesional(ProfesionalDto dto)
		{
			var usuarioDto = UsuarioMapper.DtoHijosAUsuarioDto(dto); //Pasaje a dto usuario
			var usuario = _service.CrearUsuario(usuarioDto, 2); //Se crea un usuario base model
			var result = new CreateProfesionalDomain(_unitOfWork).Validar(dto);

			if (!result.EsValido)
			{
				return new ServiceResponse<Profesional>
				{
					Errores = result.Errores
				};
			}

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				await _unitOfWork.Usuarios.AddAsyncUsuario(usuario); //Se sube al usuario base a la BD
				await _unitOfWork.CompleteAsync();

				var profesional = ProfesionalMapper.DeProfesionalDtoAProfesional(dto, usuario.IdUsuario); //Creamos un Profesional Model
				var especialidades = ProfesionalMapper.AsignacionesEspecialidades(dto, usuario.IdUsuario); //Creamos sus Especialidades Model
				var horarios = ProfesionalMapper.AsignacionesHorariosLaborales(dto, usuario.IdUsuario); //Creamos sus horarios Model

				await _unitOfWork.Profesionales.AddProfesional(profesional); //Subimos profesional a la BD
				await _unitOfWork.Profesionales.AddEspecialidades(especialidades); //Subimos sus especialidades a la BD
				await _unitOfWork.Profesionales.AddHorarios(horarios); //Subimos sus horarios Laborales a la BD

				await _unitOfWork.CompleteAsync();
				await _unitOfWork.CommitAsync(); //Todo ok y guardamos cambios

				return new ServiceResponse<Profesional>
				{
					Exito = true,
					Mensaje = "Profesional registrado con éxito",
					Cuerpo = profesional
				};
			}
			catch(Exception e)
			{
				await _unitOfWork.RollbackAsync();

				return new ServiceResponse<Profesional>
				{
					Mensaje = $"Hubo un error al registrar profesional. Inténtelo más tarde. Error: {e}",
				};

			}
		}

		public async Task<ServiceResponse<List<Profesional>>> MostrarProfesionalesPorEspecialidad(int idEspecialidad)
		{
			try
			{
				var profesionales = await _unitOfWork.Profesionales.GetProfesionalesByEspecialidad(idEspecialidad);

				return new ServiceResponse<List<Profesional>>
				{
					Exito = true,
					Mensaje = "Especialistas traidos con éxito",
					Cuerpo = profesionales
				};

			}
			catch
			{
				return new ServiceResponse<List<Profesional>>
				{
					Mensaje = "Hubo un error inesperado al intentar traer a los profesionales. Inténtelo más tarde"
				};
			}
		}
		public async Task<ServiceResponse<IEnumerable<string>>> GetFranjaHoraria(int idProfesional, string fecha)
		{
			var diaSemana = (int) DateTime.Parse(fecha).DayOfWeek;

			var horarioLaboral = await _unitOfWork.HorariosLaborales
				.FirstOrDefaultHorarioLaboral(
					idProfesional: idProfesional,
					diaSemana: diaSemana
				);

			if (horarioLaboral == null) {
				return new ServiceResponse<IEnumerable<string>>
				{
					Mensaje = "No hay horarios en esa fecha",
					Cuerpo = []
				};
			}

			var franja = FranjaHorariaHelper.FranjaHorariaProfesional(
				horarioLaboral.HoraInicio, 
				horarioLaboral.HoraFin, 
				horarioLaboral.DuracionTurno)
				.Select(h => h.ToString("HH:mm"));

			return new ServiceResponse<IEnumerable<string>>
			{
				Exito = true,
				Mensaje = "Franja horaria consultada con éxito",
				Cuerpo = franja
			};
		}
	}
}
