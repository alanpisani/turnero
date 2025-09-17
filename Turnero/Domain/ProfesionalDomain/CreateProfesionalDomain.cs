using Turnero.Common.Enums;
using Turnero.Common.Helpers;
using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Domain.ProfesionalDomain
{
	public class CreateProfesionalDomain(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;

		public DomainValidationResult Validar(ProfesionalDto dto)
		{
			var result = new DomainValidationResult();

			if (!DiasValidos(dto.HorariosLaborales))
			{
				result.AddError("HorariosLaborales", "Se ingresó un dia inválido. Elija entre el Lunes y el Domingo");
			}

			if (!HorarioCoherente(dto.HorariosLaborales)) {
				result.AddError("HorariosLaborales", "Debe elegir un horario laboral coherente");
			}

			if (!DuracionTurnoCoherente(dto.HorariosLaborales))
			{
				result.AddError("HorariosLaborales", "La duracion del turno debe estar en un rango de 15 a 30 minutos");
			}

			result.EsValido = result.Errores.Count == 0;
			return result;
		}
		private bool DiasValidos(List<HorarioLaboralDto> horariosLaborales)
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

		private bool HorarioCoherente(List<HorarioLaboralDto> horariosLaborales)
		{
			var horarioLaboralIlogico = horariosLaborales
				.Any(h => TimeOnly.Parse(h.HoraFin) < TimeOnly.Parse(h.HoraInicio));

			return !horarioLaboralIlogico;
		}

		private bool DuracionTurnoCoherente(List<HorarioLaboralDto> horariosLaborales)
		{
			var duracionTurnoIlogico = horariosLaborales
				.Any(h => h.DuracionTurno < 15 || h.DuracionTurno > 30);

			return !duracionTurnoIlogico;

		}
	}
}
