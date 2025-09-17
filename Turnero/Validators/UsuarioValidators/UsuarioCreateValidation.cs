using FluentValidation;
using Humanizer;
using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Validators.UsuarioValidators
{
	public class UsuarioCreateValidation: AbstractValidator<UsuarioDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		public UsuarioCreateValidation(IUnitOfWork unit)
		{
			_unitOfWork = unit;

			RuleFor(x => x.Nombre)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("El nombre es requerido")
				.MaximumLength(20).WithMessage("El nombre ingresado es demasiado extenso");

			RuleFor(x => x.Apellido)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("El apellido es requerido")
				.MaximumLength(20).WithMessage("El apellido ingresado es demasiado extenso");

			RuleFor(x => x.Dni)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("El DNI es requerido")
				.InclusiveBetween(10000000, 99999999).WithMessage("Debe escribir un DNI válido");

			RuleFor(x => x.Email)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("El correo electrónico es requerido")
				.Matches(@"^[^@\s]+@[^@\s]+\.(com|net|org)$").WithMessage("Debe ingresar un correo electrónico válido")
				.Length(7, 30).WithMessage("El mail debe contener entre 7 a 30 caracteres")
				.MustAsync(async (email, CancellationToken) =>
					!await _unitOfWork.Usuarios
					.AnyUsuarioByEmail(email)
				).WithMessage("El mail ingresado ya se encuentra registrado en el sistema. Ingrese otro");

			RuleFor(x => x.FechaNacimiento)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("La fecha de nacimiento es requerida")
				.Must((fecha) =>
					DateOnly.TryParse(fecha, out _)).WithMessage("Debe ingresar una fecha de nacimiento válida")
				.Must((fecha) =>
					DateOnly.Parse(fecha) < DateOnly.FromDateTime(DateTime.Now)).WithMessage("Debe ingresar una fecha de nacimiento que haya pasado");

			RuleFor(x => x.Contrasenia)
				.NotEmpty().WithMessage("La contraseña es requerida");

			RuleFor(x => x.ContraseniaRepetida)
				.NotEmpty().WithMessage("Este campo es obligatorio")
				.Equal(x => x.Contrasenia).WithMessage("Las contraseñas no coinciden");
			
		}
	}
}
