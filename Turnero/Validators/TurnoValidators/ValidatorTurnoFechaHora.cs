using Turnero.Common.Helpers;
using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Validators.TurnoValidators
{
	public class ValidatorTurnoFechaHora(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;

		public async Task<ValidationResult> ValidarFechasYHoras(TurnoDto dto)
		{
			var result = new ValidationResult();

			if (!EsFechaValida(dto)) result.Mensajes.Add("No se ingresó una fecha válida para el turno");
			
			if (!EsHoraValida(dto)) result.Mensajes.Add("No se ingresó un horario válido para el turno");
			
			if (!EsFechaFutura(dto)) result.Mensajes.Add("La fecha ingresada ya pasó. Elija una fecha futura");
			
			if (!await EsEnHorarioLaboral(dto)) result.Mensajes.Add("El horario ingresado no coincide con el horario laboral del profesional");
			
			if (!await EsTurnoDisponible(dto)) result.Mensajes.Add("El turno está ocupado. Intente con otra fecha y horario");
			
			if (!await EncajaEnFranjaHoraria(dto)) result.Mensajes.Add("El profesional no trabaja en ese horario");
			
			result.EsValido = result.Mensajes.Count == 0;
			return result;
		}

		private bool EsFechaValida(TurnoDto dto)
		{
			return DateOnly.TryParse(dto.Dia, out _);
		}

		private bool EsHoraValida(TurnoDto dto)
		{
			return TimeOnly.TryParse(dto.Hora, out _);
		}

		private bool EsFechaFutura(TurnoDto dto)
		{
			try
			{
				var fecha = DateOnly.Parse(dto.Dia);
				var hora = TimeOnly.Parse(dto.Hora);
				var fechaYHora = fecha.ToDateTime(hora);

				return fechaYHora >= DateTime.Now;
			}
			catch
			{
				return false;
			}
		}

		private async Task<bool> EsEnHorarioLaboral(TurnoDto dto) {
			try
			{
				var hora = TimeOnly.Parse(dto.Hora);

				var esHoraTurnoValida = await _unitOfWork.HorariosLaborales.AnyHoraDentroDeFranja(
					idProfesional: dto.IdProfesional,
					horaIngresada: hora
				);

				return esHoraTurnoValida;
			}
			catch { return false; }

		}

		private async Task<bool> EsTurnoDisponible(TurnoDto dto)
		{
			try
			{
				var fecha = DateOnly.Parse(dto.Dia);
				var hora = TimeOnly.Parse(dto.Hora);
				var fechaYHora = fecha.ToDateTime(hora);

				bool ocupado = await _unitOfWork.Turnos.AnyTurnoOcupado(
					idProfesional: dto.IdProfesional,
					fechaYHoraIngresada: fechaYHora
					);

				return !ocupado;
			}
			catch { return false;  }

		}

		private async Task<bool> EncajaEnFranjaHoraria(TurnoDto dto)
		{
			try
			{
				var fecha = DateOnly.Parse(dto.Dia);
				var hora = TimeOnly.Parse(dto.Hora);
				var diaSemana = (int)fecha.ToDateTime(hora).DayOfWeek;

				var horarios = await _unitOfWork.HorariosLaborales.FirstOrDefaultHorarioLaboral(
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
			catch { return false; }

		}
	}
} 
