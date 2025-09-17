using Turnero.Common.Enums;
using Turnero.Dto;

namespace Turnero.Common.Helpers.ProfesionalValidatorHelper
{
	/// <summary>
	/// Helper que me ayuda a modularizar validaciones para la lógica de formato o logica simple en cuanto a los HORARIOS LABORALES DEL PROFESIONAL A CREAR
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
	}
}
