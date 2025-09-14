using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Validators.TurnoValidators
{
	public class TurnoValidator(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit; 
		public async Task<ValidationResult> ValidarTurno(TurnoDto dto)
		{
			var result = new ValidationResult();
			var datosValidator = await new ValidatorTurnoDatosExistentes(_unitOfWork).ValidarTurnoDatosExistentes(dto);
			var fechaHoraValidator = await new ValidatorTurnoFechaHora(_unitOfWork).ValidarFechasYHoras(dto);

			if (!datosValidator.EsValido)
			{
				result.Mensajes.AddRange(datosValidator.Mensajes);
			}
			if (!fechaHoraValidator.EsValido) {
				result.Mensajes.AddRange(fechaHoraValidator.Mensajes);
			}

			result.EsValido = result.Mensajes.Count == 0;
			return result;
		}
	}
}
