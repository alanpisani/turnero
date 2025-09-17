using FluentValidation;
using Turnero.Common.Helpers.TurnoValidatorHelper;
using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Validators.TurnoValidators
{
	public class TurnoCreateValidation: AbstractValidator<TurnoDto>
	{
		private readonly IUnitOfWork _unitOfWork;

		public TurnoCreateValidation(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

			RuleFor(x => x.IdPaciente)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("Paciente es requerido para solicitar turno")
				.MustAsync((id, CancellationToken) => TurnoDatosValidationHelper.PacienteExiste(id, _unitOfWork))
				.WithMessage("El paciente ingresado no se encuentra registrado en el sistema");

			RuleFor(x => x.IdEspecialidad)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("Especialidad requerida para solicitar turno")
				.MustAsync((id, CancellationToken) => TurnoDatosValidationHelper.EspecialidadExiste(id, _unitOfWork))
				.WithMessage("La especialidad seleccionada no se encuentra registrada en el sistema"); ;

			RuleFor(x => x.IdProfesional)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("Profesional requerido para solicitar turno")
				.MustAsync((id, CancellationToken) => TurnoDatosValidationHelper.ProfesionalExiste(id, _unitOfWork))
				.WithMessage("El profesional seleccionado no se encuentra registrado en el sistema");

			RuleFor(x => x.Dia)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("Elija un dia")
				.Must(TurnoFechaHoraValidationhelper.EsDiaValido).WithMessage("No se ingresó un dia válido para el turno");

			RuleFor(x => x.Hora)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("Elija un horario")
				.Must(TurnoFechaHoraValidationhelper.EsHoraValida).WithMessage("No se ingresó una hora válida para el turno");

			//Reglas mas complejas. Requieren de dos atributos a validar o más
			RuleFor(x => x)
				.Cascade(CascadeMode.Stop)
				.MustAsync((dto, CancellationToken) => TurnoDatosValidationHelper.ProfesionalTieneEsaEspecialidad(dto, _unitOfWork))
					.WithMessage("El profesional no posee la especialidad elegida")
				.Must(TurnoFechaHoraValidationhelper.EshechaYHoraValida).WithMessage("La fecha y hora ingresadas no son válidas")
				.Must(TurnoFechaHoraValidationhelper.EsFechaFutura).WithMessage("La fecha ingresada para el turno es antigua. Elija una fecha a futuro")
				.MustAsync((dto, CancellationToken) => TurnoFechaHoraValidationhelper.EsEnHorarioLaboral(dto, _unitOfWork))
					.WithMessage("El horario ingresado no coincide con el horario laboral del profesional")
				.MustAsync((dto, CancellationToken) => TurnoFechaHoraValidationhelper.EsTurnoDisponible(dto, _unitOfWork))
					.WithMessage("El turno está ocupado. Intente con otra fecha y horario")
				.MustAsync((dto, CancellationToken) => TurnoFechaHoraValidationhelper.EncajaEnFranjaHoraria(dto, _unitOfWork))
					.WithMessage("El profesional no trabaja en ese horario");
		}
	}
}
