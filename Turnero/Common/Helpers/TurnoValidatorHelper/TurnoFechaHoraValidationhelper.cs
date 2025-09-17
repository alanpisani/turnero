using Turnero.Dto;
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

		public static bool EshechaYHoraValida(TurnoDto dto)
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

		public static bool EsFechaFutura(TurnoDto dto)
		{
			var fecha = DateOnly.Parse(dto.Dia);
			var hora = TimeOnly.Parse(dto.Hora);
			var fechaYHora = fecha.ToDateTime(hora);

			return fechaYHora >= DateTime.Now;
			
		}

		public static async Task<bool> EsEnHorarioLaboral(TurnoDto dto, IUnitOfWork unitOfWork)
		{
	
			var hora = TimeOnly.Parse(dto.Hora);

			var esHoraTurnoValida = await unitOfWork.HorariosLaborales.AnyHoraDentroDeFranja(
				idProfesional: dto.IdProfesional,
				horaIngresada: hora
			);

			return esHoraTurnoValida;
			
		}

		public static async Task<bool> EsTurnoDisponible(TurnoDto dto, IUnitOfWork unitOfWork)
		{
			var fecha = DateOnly.Parse(dto.Dia);
			var hora = TimeOnly.Parse(dto.Hora);
			var fechaYHora = fecha.ToDateTime(hora);

			bool ocupado = await unitOfWork.Turnos.AnyTurnoOcupado(
				idProfesional: dto.IdProfesional,
				fechaYHoraIngresada: fechaYHora
			);

			return !ocupado;
		}

		public static async Task<bool> EncajaEnFranjaHoraria(TurnoDto dto, IUnitOfWork unitOfWork)
		{
			var fecha = DateOnly.Parse(dto.Dia);
			var hora = TimeOnly.Parse(dto.Hora);
			var diaSemana = (int)fecha.ToDateTime(hora).DayOfWeek;

			var horarios = await unitOfWork.HorariosLaborales.FirstOrDefaultHorarioLaboral(
				idProfesional: dto.IdProfesional,
				diaSemana: diaSemana
				);

			if (horarios == null) return false;

			return FranjaHorariaHelper.FranjaHorariaProfesional(
				inicio: horarios.HoraInicio,
				fin: horarios.HoraFin,
				duracionMinutos: horarios.DuracionTurno)
				.Any(h => h == hora);
		}
	}
}
