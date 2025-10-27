 using FluentValidation;
using Turnero.Dto;

namespace Turnero.Validators.UsuarioValidators
{
	public class UsuarioRapidoValidation : AbstractValidator<UsuarioRapidoDto>
	{
		public UsuarioRapidoValidation()
		{
			
			RuleFor(x => x.Nombre)
				.NotEmpty().WithMessage("El nombre es requerido")
				.MaximumLength(20).WithMessage("El nombre ingresado es demasiado extenso");

			RuleFor(x => x.Dni)
				.NotEmpty().WithMessage("El DNI es requerido")
				.InclusiveBetween(10000000, 99999999).WithMessage("Debe ingresar un DNI válido");
		}
	}
}
