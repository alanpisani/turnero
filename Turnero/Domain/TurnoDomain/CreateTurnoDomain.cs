using Turnero.Common.Helpers;
using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Domain.TurnoDomain
{
	public class CreateTurnoDomain(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;

		public async Task<DomainValidationResult> ValidarLogicaNegocio(TurnoDto dto)
		{
			var result = new DomainValidationResult();

			if(!await ProfesionalTieneEsaEspecialidad(dto))
			{
				result.AddError("IdEspecialidad", "El profesional no posee la especialidad elegida");
			}

			if (!EsFechaFutura(dto))
			{
				result.AddError("Dia", "La fecha ingresada para el turno es antigua. Elija una fecha a futuro");
			}

			if(!await EsEnHorarioLaboral(dto))
			{
				result.AddError("Hora", "El horario ingresado no coincide con el horario laboral del profesional");
			}

			if(!await EsTurnoDisponible(dto))
			{
				result.AddError("Hora", "El turno está ocupado. Intente con otra fecha y horario");
			}

			if(!await EncajaEnFranjaHoraria(dto))
			{
				result.AddError("Hora", "El profesional no trabaja en ese horario");
			}

			result.EsValido = result.Errores.Count == 0;
			return result;
		}

		private async Task<bool> ProfesionalTieneEsaEspecialidad(TurnoDto dto)
		{
			return await _unitOfWork.Profesionales.AnyProfesionalWithThatSpeciality(
				idProfesional: dto.IdProfesional,
				idEspecialidad: dto.IdEspecialidad
				);
		}

		private static bool EsFechaFutura(TurnoDto dto)
		{
			var fecha = DateOnly.Parse(dto.Dia);
			var hora = TimeOnly.Parse(dto.Hora);
			var fechaYHora = fecha.ToDateTime(hora);

			return fechaYHora >= DateTime.Now;

		}

		private async Task<bool> EsEnHorarioLaboral(TurnoDto dto)
		{
			var hora = TimeOnly.Parse(dto.Hora);

			var esHoraTurnoValida = await _unitOfWork.HorariosLaborales.AnyHoraDentroDeFranja(
				idProfesional: dto.IdProfesional,
				horaIngresada: hora
			);

			return esHoraTurnoValida;

		}

		private async Task<bool> EsTurnoDisponible(TurnoDto dto)
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

		private async Task<bool> EncajaEnFranjaHoraria(TurnoDto dto)
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
	}
}
