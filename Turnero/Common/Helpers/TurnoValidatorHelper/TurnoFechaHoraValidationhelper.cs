using Turnero.Dto.TurnoDto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Common.Helpers.TurnoValidatorHelper
{
    public static class TurnoFechaHoraValidationhelper
	{
		public static bool EsDiaValido(string dia)
		{
			return DateOnly.TryParse(dia, out _);
		}

		public static bool EsHoraValida(string hora)
		{
			return TimeOnly.TryParse(hora, out _);
		}

		public static bool EshechaYHoraValida(TurnoRequestDto dto)
		{
			try
			{
				var fecha = DateOnly.Parse(dto.Dia);
				var hora = TimeOnly.Parse(dto.Hora);
				var fechaYHora = fecha.ToDateTime(hora);

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
