using Turnero.Dto;
using Turnero.Repositories.Interfaces;
using Turnero.Validators.UsuarioValidators;

namespace Turnero.Validators.PacienteValidators
{
	public class PacienteValidator(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;
		public async Task<ValidationResult> ValidarPaciente(PacienteDto dto)
		{
			var result = new ValidationResult();
			var validacionUsuario = await new ValidatorUsuario(_unitOfWork).ValidarUsuario(dto);
			var validacionCoberturas = await new ValidatorCobertura(_unitOfWork).ValidarCoberturas(dto);
			bool bandera = false;

			if (!validacionUsuario.EsValido)
			{
				bandera = true;
				result.Mensajes.AddRange(validacionUsuario.Mensajes);
			}

			if (!validacionCoberturas.EsValido)
			{
				result.Mensajes.AddRange(validacionCoberturas.Mensajes);
				bandera = true;
			}

			if (bandera)
			{
				return result;
			}

			result.EsValido = true;
			return result;

		}
	}
}
