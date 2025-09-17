using FluentValidation;
using Turnero.Common.Helpers.ProfesionalValidatorHelper;
using Turnero.Dto;
using Turnero.Repositories.Interfaces;
using Turnero.Validators.UsuarioValidators;

namespace Turnero.Validators.ProfesionalValidators
{
	public class ProfesionalCreateValidation: AbstractValidator<ProfesionalDto>
	{
		public ProfesionalCreateValidation(IUnitOfWork unit) {

			Include(new UsuarioCreateValidation(unit));

			RuleFor(x => x.Matricula)
				.NotEmpty().WithMessage("La matricula es requerida");

			RuleFor(x => x.Especialidades)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("Debe asignarle al menos una especialidad al profesional")
				.Must(ProfesionalEspecialidadValidatorHelper.SinEspecialidadesRepetidas).WithMessage("Se ingresaron dos o más veces la misma especialidad para el profesional")
				.MustAsync((lista, CancellationToken) =>
					ProfesionalEspecialidadValidatorHelper.EspecialidadExiste(lista, unit)).WithMessage("La especialidad ingresada no existe");

			RuleFor(x => x.HorariosLaborales)
				.Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("Debe haber al menos un horario laboral")
				.Must(ProfesionalHorariosLaboralesValidatorHelper.SinHorariosDuplicados).WithMessage("Se ingresaron dos horarios iguales en el mismo dia")
				.Must(ProfesionalHorariosLaboralesValidatorHelper.EsHorarioLaboral).WithMessage("No se ingresó un horario correcto");
		}
	}
}
