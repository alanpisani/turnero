using Microsoft.EntityFrameworkCore;
using Turnero.Dto;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Validators.UsuarioValidators
{
	public class ValidatorUsuario(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;

		public async Task<ValidationResult> ValidarUsuario(UsuarioDto dto)
		{
			var result = new ValidationResult();

			if (await ElUsuarioExisteEnBD(dto)) {
				result.Mensajes.Add("El mail ingresado ya se encuentra registrado en el sistema. Ingrese otro");
			}
			if (FechaNacimientoInvalida(dto))
			{
				result.Mensajes.Add("Debe ingresar una fecha de nacimiento válida");
			}

			result.EsValido = result.Mensajes.Count == 0;
			return result;
		}

		private async Task<bool> ElUsuarioExisteEnBD(UsuarioDto dto) {
			
			return await _unitOfWork.Usuarios
				.AnyUsuarioByEmail(dto.Email);
		}

		private bool FechaNacimientoInvalida(UsuarioDto dto) {

			return !DateOnly.TryParse(dto.FechaNacimiento, out _);
			
		}
	}
}
