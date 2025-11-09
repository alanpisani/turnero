using FluentValidation;
using Turnero.Dto.Usuario;
using Turnero.Repositories.Interfaces;

namespace Turnero.Validators.UsuarioValidators
{
    /// <summary>
    /// Validador de registro de usuario. Soporta usuarios completos e incompletos.
    /// </summary>
    public class UsuarioCreateValidation : AbstractValidator<UsuarioRequestDto>
	{
		private readonly IUnitOfWork _unitOfWork;

		public UsuarioCreateValidation(IUnitOfWork unit)
		{
			_unitOfWork = unit;

			// Nombre siempre es requerido, incluso para usuario ràpido
			RuleFor(x => x.Nombre)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("El nombre es requerido")
				.MaximumLength(20).WithMessage("El nombre ingresado es demasiado extenso")
					.When(x => !string.IsNullOrEmpty(x.Nombre));

			RuleFor(x => x.Apellido)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("El apellido es requerido")
				.MaximumLength(20).WithMessage("El apellido ingresado es demasiado extenso")
					.When(x => !string.IsNullOrEmpty(x.Apellido));

			RuleFor(x => x.Dni)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("El DNI es requerido")
				.InclusiveBetween(10000000, 99999999).WithMessage("Debe escribir un DNI válido");

			RuleFor(x => x.Email)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("El correo electrónico es requerido")
				.Matches(@"^[^@\s]+@[^@\s]+\.(com|net|org)$").WithMessage("Debe ingresar un correo electrónico válido")
					.When(x => !string.IsNullOrEmpty(x.Email))
				.Length(7, 30).WithMessage("El mail debe contener entre 7 a 30 caracteres")
					.When(x => !string.IsNullOrEmpty(x.Email))
				.MustAsync(async (email, CancellationToken) =>
					!await _unitOfWork.Usuarios.AnyUsuarioByEmail(email)
				).WithMessage("El mail ingresado ya se encuentra registrado en el sistema. Ingrese otro")
					.When(x => !string.IsNullOrEmpty(x.Email));

			RuleFor(x => x.Contrasenia)
				.NotEmpty().WithMessage("La contraseña es requerida");

			RuleFor(x => x.ContraseniaRepetida)
				.NotEmpty().WithMessage("Este campo es obligatorio")
				.Equal(x => x.Contrasenia).WithMessage("Las contraseñas no coinciden");
		}
	}
}
