using Turnero.Dto.Profesional;
using Turnero.Models;

namespace Turnero.Mappers
{
    public class ProfesionalMapper
	{
		public static Profesional DeProfesionalDtoAProfesional(ProfesionalRequestDto dto, int id)
		{
			return new Profesional
			{
				IdUsuario= id,
				Matricula= dto.Matricula
			};
		}

		public static ProfesionalResponseDto ToResponseDto(Profesional profesional) {
			return new ProfesionalResponseDto { 
				IdUsuario= profesional.IdUsuario,
				NombreProfesional= profesional.IdUsuarioNavigation.Nombre,
				ApellidoProfesional=profesional.IdUsuarioNavigation.Apellido,
				Matricula= profesional.Matricula
			};
		
		}

		public static IEnumerable<ProfesionalEspecialidad> AsignacionesEspecialidades(ProfesionalRequestDto dto, int id)
		{
			foreach (var especialidad in dto.Especialidades)
			{

				yield return new ProfesionalEspecialidad

				{
					IdProfesional = id,
					IdEspecialidad = especialidad,
				};


			}
		}

		public static IEnumerable<HorarioLaboral> AsignacionesHorariosLaborales(ProfesionalRequestDto dto, int id)
		{
			foreach (var horario in dto.HorariosLaborales)
			{
				yield return new HorarioLaboral
				{
					IdProfesional = id,
					DiaSemana = (sbyte)horario.DiaLaboral,
					HoraInicio = TimeOnly.Parse(horario.HoraInicio),
					HoraFin = TimeOnly.Parse(horario.HoraFin),
					DuracionTurno = (sbyte)horario.DuracionTurno,
					Activo = true
				};
			}
		}
	}
}
