using Turnero.Dto;
using Turnero.Models;

namespace Turnero.Mappers
{
	public class ProfesionalMapper
	{
		public static Profesional DeProfesionalDtoAProfesional(ProfesionalDto dto, int id)
		{
			return new Profesional
			(
				IdUsuario: id,
				Matricula: dto.Matricula
			);
		}

		public static IEnumerable<ProfesionalEspecialidad> AsignacionesEspecialidades(ProfesionalDto dto, int id)
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

		public static IEnumerable<HorarioLaboral> AsignacionesHorariosLaborales(ProfesionalDto dto, int id)
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
