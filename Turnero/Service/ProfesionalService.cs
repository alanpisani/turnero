using Turnero.Common.Enums;
using Turnero.Common.Helpers;
using Turnero.Domain.ProfesionalDomain;
using Turnero.Dto;
using Turnero.Dto.Profesional;
using Turnero.Exceptions;
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

		public async Task<Profesional> RegistrarProfesional(ProfesionalRequestDto dto)
		{
			var usuario = _service.CrearUsuario(dto, RolesUsuario.Profesional.ToString()); //Se crea un usuario base model

			var result = new CreateProfesionalDomain(_unitOfWork).Validar(dto);


			if (!result.EsValido) throw new BussinessErrorContentException(result.Errores);

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

				return profesional;
			}
			catch
			{
				await _unitOfWork.RollbackAsync();

				throw new Exception("Hubo un error al registrar profesional. Inténtelo más tarde.");

			}
		}

		public async Task<ResponseDto<List<ProfesionalResponseDto>>> MostrarProfesionales()
		{
			var profesionales = await _unitOfWork.Profesionales.GetAllProfesionals();

			return new ResponseDto<List<ProfesionalResponseDto>> { 
				Success = true,
				Message = "Profesionales consultados con éxito",
				Data = profesionales.Select(p => ProfesionalMapper.ToResponseDto(p)).ToList()

			};
		}

		public async Task<ResponseDto<ProfesionalResponseDto>> MostrarProfesionalPorId(int idProfesional)
		{
			var profesional = await _unitOfWork.Profesionales.GetProfesionalById(idProfesional);

			if (profesional == null) throw new NotFoundException($"No se encontró profesional con id: {idProfesional}");

			return new ResponseDto<ProfesionalResponseDto> { 
				Success = true,
				Message = "Profesional traido con éxito",
				Data= ProfesionalMapper.ToResponseDto(profesional)
			};
		}

		public async Task<ResponseDto<List<ProfesionalResponseDto>>> MostrarProfesionalesPorEspecialidad(int idEspecialidad)
		{
			var hayEspecialidad = await _unitOfWork.Especialidades.AnyEspecialidad(idEspecialidad);

			if (!hayEspecialidad) throw new NotFoundException("No se encontró esa especialidad en la base de datos");

			try
			{

				var profesionales = await _unitOfWork.Profesionales.GetProfesionalesByEspecialidad(idEspecialidad);

				return new ResponseDto<List<ProfesionalResponseDto>>
				{
					Success = true,
					Message = "Profesionales consultados con éxito",
					Data = profesionales.Select(p => ProfesionalMapper.ToResponseDto(p)).ToList()

				};

			}
			catch(Exception ex)
			{
				throw new Exception($"Hubo un error inesperado al intentar traer a los profesionales. Inténtelo más tarde. Error: {ex.Message}");
			}
		}
		public async Task<ResponseDto<IEnumerable<string>>> GetFranjaHoraria(int idProfesional, string fecha) //DESARROLLAR MAS
		{

			var hayProfesional = await _unitOfWork.Profesionales.AnyProfesional(idProfesional);

			if (!hayProfesional) throw new NotFoundException($"No se encontró profesional con id: {idProfesional}");

			var diaSemana = (int) DateTime.Parse(fecha).DayOfWeek;

			var horarioLaboral = await _unitOfWork.HorariosLaborales
				.FirstOrDefaultHorarioLaboral(
					idProfesional: idProfesional,
					diaSemana: diaSemana
				);

			if (horarioLaboral == null) throw new NotFoundException("No hay horarios en esa fecha");

			var franja = FranjaHorariaHelper.FranjaHorariaProfesional(
				horarioLaboral.HoraInicio, 
				horarioLaboral.HoraFin, 
				horarioLaboral.DuracionTurno)
				.Select(h => h.ToString("HH:mm")).ToList();

			var turnosEseDia = await _unitOfWork.Turnos
				.GetTurnosByProfesionalAndFecha(idProfesional, DateOnly.Parse(fecha));

			var horariosOcupados = turnosEseDia!
				.Where(t => t.EstadoTurno == EnumEstadoTurno.Solicitado.ToString())
				.Select(t => new TimeOnly(t.FechaTurno.Hour, t.FechaTurno.Minute).ToString("HH:mm"))
				.ToList();

			return new ResponseDto<IEnumerable<string>>{
				Success=true,
				Message= "Franja horaria consultada con éxito",
				Data= franja.Except(horariosOcupados)
			};
		
		}

		public async Task<ResponseDto<List<string>>> GetDiasDisponiblesProfesional(int idProfesional) {

			var hayProfesional = await _unitOfWork.Profesionales.AnyProfesional(idProfesional);

			if (!hayProfesional) throw new NotFoundException($"No se encontró profesional con id: {idProfesional}");

			//Lógica de negocio más íntima. Desarrollar mejor a futuro

			var hoy = DateOnly.FromDateTime(DateTime.UtcNow);
			var hasta = hoy.AddDays(30);

			var horariosProfesional = await _unitOfWork.HorariosLaborales.GetAllByProfesional(idProfesional);

			List<sbyte> diasSemanales = horariosProfesional.Select(h=> h.DiaSemana).ToList();

			var diasDisponibles = new List<string>();

			while (hoy < hasta) { 
				if(diasSemanales.Contains((sbyte)hoy.DayOfWeek))
				{
					diasDisponibles.Add(hoy.ToString("yyyy-MM-dd"));
				}
				hoy = hoy.AddDays(1);
			}

			return new ResponseDto<List<string>> { 
				Success = true,
				Message = $"Dias disponibles del profesional con id: {idProfesional} consultados con éxito",
				Data = diasDisponibles
			}; 
		}
	}
}
