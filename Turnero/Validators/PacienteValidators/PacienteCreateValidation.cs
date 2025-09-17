using FluentValidation;
using Turnero.Common.Helpers.PacienteValidatorHelper;
using Turnero.Dto;
using Turnero.Repositories.Interfaces;
using Turnero.Validators.UsuarioValidators;

namespace Turnero.Validators.PacienteValidators
{
	public class PacienteCreateValidation: AbstractValidator<PacienteDto>
	{
		public PacienteCreateValidation(IUnitOfWork unit) {

			Include(new UsuarioCreateValidation(unit));

			RuleFor(x => x.CoberturasMedicas)
				.Must(PacienteValidatorHelper.NoHayCoberturasRepetidasEnRegistro).WithMessage("No se pueden agregar dos veces la misma cobertura médica")
				.MustAsync((lista, CancellationToken) =>
					PacienteValidatorHelper.ObraSocialSinRepetirEnRegistro(lista, unit)).WithMessage("No se pueden agregar dos obras sociales");
		}
	}
}
