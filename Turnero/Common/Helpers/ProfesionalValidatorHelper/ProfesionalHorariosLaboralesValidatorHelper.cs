using Turnero.Common.Enums;
using Turnero.Dto;

namespace Turnero.Common.Helpers.ProfesionalValidatorHelper
{
	/// <summary>
	/// Helper que me ayuda a modularizar validaciones para la lógica del negocio en cuanto a los HORARIOS LABORALES DEL PROFESIONAL A CREAR
	/// </summary>
	public static class ProfesionalHorariosLaboralesValidatorHelper
	{
		public static bool SinHorariosDuplicados(List<HorarioLaboralDto> horariosLaborales)
		{
			var horariosDuplicados = horariosLaborales
				.GroupBy(hl => new { hl.DiaLaboral, hl.HoraInicio, hl.HoraFin })
				.Where(g => g.Count() > 1)
				.SelectMany(g => g)
				.ToList();

			return horariosDuplicados.Count == 0;
		}

		public static bool DiasValidos(List<HorarioLaboralDto> horariosLaborales)
		{

			foreach (var horarioLaboral in horariosLaborales)
			{
				if (!Enum.IsDefined(typeof(DiaSemana), horarioLaboral.DiaLaboral))
				{
					return false;
				}
			}
			return true;
		}

		public static bool EsHorarioLaboral(List<HorarioLaboralDto> horariosLaborales)
		{
			foreach (var algo in horariosLaborales)
			{
				if (!TimeOnly.TryParse(algo.HoraFin, out _) || !TimeOnly.TryParse(algo.HoraInicio, out _))
				{
					return false;
				}
			}

			return true;
		}

		public static bool HorarioCoherente(List<HorarioLaboralDto> horariosLaborales)
		{
			//if (!EsHorarioLaboral(dto)) return false;

			var horarioLaboralIlogico = horariosLaborales
				.Any(h => TimeOnly.Parse(h.HoraFin) < TimeOnly.Parse(h.HoraInicio));

			return !horarioLaboralIlogico;

		}

		public static bool DuracionTurnoCoherente(List<HorarioLaboralDto> horariosLaborales)
		{
			var duracionTurnoIlogico = horariosLaborales
				.Any(h => h.DuracionTurno < 15 || h.DuracionTurno > 30);

			return !duracionTurnoIlogico;

		}
	}
}
