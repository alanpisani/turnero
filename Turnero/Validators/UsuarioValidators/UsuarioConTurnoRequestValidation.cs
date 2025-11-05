using FluentValidation;
using Turnero.Common.Helpers.TurnoValidatorHelper;
using Turnero.Dto.Paciente;
using Turnero.Repositories.Interfaces;

namespace Turnero.Validators.UsuarioValidators
{
	public class UsuarioConTurnoRequestValidation: AbstractValidator<PacienteConTurnoRequestDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		public UsuarioConTurnoRequestValidation(IUnitOfWork unit)
		{
			_unitOfWork = unit;

			// Nombre siempre es requerido, incluso para usuario ràpido
			RuleFor(x => x.Nombre)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("El nombre es requerido")
				.MaximumLength(20).WithMessage("El nombre ingresado es demasiado extenso")
					.When(x => !string.IsNullOrEmpty(x.Nombre));

			// Apellido solo si se completò
			RuleFor(x => x.Apellido)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("El apellido es requerido")
					.When(x => x.IsComplete == true)
				.MaximumLength(20).WithMessage("El apellido ingresado es demasiado extenso")
					.When(x => !string.IsNullOrEmpty(x.Apellido));

			// DNI siempre requerido, incluso para usuario rápido
			RuleFor(x => x.Dni)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("El DNI es requerido")
				.InclusiveBetween(10000000, 99999999).WithMessage("Debe escribir un DNI válido")
				.MustAsync(async (dni, CancellationToken) =>
					!await _unitOfWork.Usuarios.AnyUsuarioByDni(dni)
				).WithMessage("El DNI ingresado ya se encuentra registrado en el sistema");

			// Email solo si completò
			RuleFor(x => x.Email)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("El correo electrónico es requerido")
					.When(x => x.IsComplete == true)
				.Matches(@"^[^@\s]+@[^@\s]+\.(com|net|org)$").WithMessage("Debe ingresar un correo electrónico válido")
					.When(x => !string.IsNullOrEmpty(x.Email))
				.Length(7, 30).WithMessage("El mail debe contener entre 7 a 30 caracteres")
					.When(x => !string.IsNullOrEmpty(x.Email))
				.MustAsync(async (email, CancellationToken) =>
					!await _unitOfWork.Usuarios.AnyUsuarioByEmail(email)
				).WithMessage("El mail ingresado ya se encuentra registrado en el sistema. Ingrese otro")
					.When(x => !string.IsNullOrEmpty(x.Email));

			// Contraseña solo si completò
			RuleFor(x => x.Contrasenia)
				.NotEmpty().WithMessage("La contraseña es requerida")
					.When(x => x.IsComplete == true);

			RuleFor(x => x.ContraseniaRepetida)
				.NotEmpty().WithMessage("Este campo es obligatorio")
					.When(x => x.IsComplete == true)
				.Equal(x => x.Contrasenia).WithMessage("Las contraseñas no coinciden")
					.When(x => x.IsComplete == true);

			RuleFor(x => x.Turno.IdEspecialidad)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("Especialidad requerida para solicitar turno")
				.MustAsync((id, CancellationToken) => TurnoDatosValidationHelper.EspecialidadExiste(id, _unitOfWork))
				.WithMessage("La especialidad seleccionada no se encuentra registrada en el sistema"); ;

			RuleFor(x => x.Turno.IdProfesional)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("Profesional requerido para solicitar turno")
				.MustAsync((id, CancellationToken) => TurnoDatosValidationHelper.ProfesionalExiste(id, _unitOfWork))
				.WithMessage("El profesional seleccionado no se encuentra registrado en el sistema");

			RuleFor(x => x.Turno.Dia)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("Elija un dia")
				.Must(TurnoFechaHoraValidationhelper.EsDiaValido).WithMessage("No se ingresó un dia válido para el turno");

			RuleFor(x => x.Turno.Hora)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("Elija un horario")
				.Must(TurnoFechaHoraValidationhelper.EsHoraValida).WithMessage("No se ingresó una hora válida para el turno");

			//Reglas mas complejas. Requieren de dos atributos a validar o más
			RuleFor(x => x.Turno)
				.Must(TurnoFechaHoraValidationhelper.EshechaYHoraValida).WithMessage("La fecha y hora ingresadas no son válidas");
		}
	}
}
