using Turnero.Common.Enums;
using Turnero.Dto;

namespace Turnero.Validators.ProfesionalValidators
{
	public class ProfesionalHorarioValidator()
	{
		public ValidationResult ValidarHorarios(ProfesionalDto dto)
		{
			var validation = new ValidationResult();

			if (HayHorariosDuplicados(dto))
			{
				validation.Mensajes.Add("Se ingresaron dos horarios iguales en el mismo dia");
			}
			if (!DiaValido(dto)) {
				validation.Mensajes.Add("Se ingresó un dia inválido. Elija entre el Lunes y el Domingo");
			}

			if (!EsHorarioLaboral(dto))
			{
				validation.Mensajes.Add("No se ingresó un horario correcto");
			}

            if (!HorarioCoherente(dto)){
				validation.Mensajes.Add("Debe elegir un horario laboral coherente");
			}

			if (!DuracionTurnoCoherente(dto)) {
				validation.Mensajes.Add("La duracion del turno debe estar en un rango de 15 a 30 minutos");
			}

			validation.EsValido = validation.Mensajes.Count == 0;
            return validation;
		}

		private bool HayHorariosDuplicados(ProfesionalDto dto)
		{
			var horariosDuplicados = dto.HorariosLaborales
				.GroupBy(hl => new { hl.DiaLaboral, hl.HoraInicio, hl.HoraFin })
				.Where(g => g.Count() > 1)
				.SelectMany(g => g)
				.ToList();

			return horariosDuplicados.Count > 0;
		}

		private bool DiaValido(ProfesionalDto dto) {

			foreach (var horarioLaboral in dto.HorariosLaborales)
			{
				if (!Enum.IsDefined(typeof(DiaSemana), horarioLaboral.DiaLaboral))
				{
					return false;
				}
			}
			return true;
		}

		private bool EsHorarioLaboral(ProfesionalDto dto)
		{
			foreach (var algo in dto.HorariosLaborales)
			{
				if (!TimeOnly.TryParse(algo.HoraFin, out _) || !TimeOnly.TryParse(algo.HoraInicio, out _))
				{
					return false;
				}
			}

			return true;
		}

		private bool HorarioCoherente(ProfesionalDto dto)
		{
			if (!EsHorarioLaboral(dto)) return false;

			var horarioLaboralIlogico = dto.HorariosLaborales
				.Any(h => TimeOnly.Parse(h.HoraFin) < TimeOnly.Parse(h.HoraInicio));

			return !horarioLaboralIlogico;
			
		}

		private bool DuracionTurnoCoherente(ProfesionalDto dto)
		{
			var duracionTurnoIlogico = dto.HorariosLaborales
				.Any(h => h.DuracionTurno < 15 || h.DuracionTurno > 30);

			return !duracionTurnoIlogico;

		}

		
	}
}
