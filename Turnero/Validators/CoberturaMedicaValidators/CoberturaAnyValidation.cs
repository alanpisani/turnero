using FluentValidation;
using Turnero.Common.Helpers.CoberturaValidatorHelper;
using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Validators.CoberturaMedicaValidators
{
	public class CoberturaAnyValidation: AbstractValidator<CoberturaMedicaDto>
	{
		private readonly IUnitOfWork _unitOfWork;

		public CoberturaAnyValidation(IUnitOfWork unitOfWork)
		{
			this._unitOfWork = unitOfWork;

			RuleFor(x => x.Nombre)
				.MustAsync((nombre, CancellationToken) => CoberturaValidationHelper.ExisteCobertura(nombre, _unitOfWork))
				.WithMessage("La cobertura médica no existe");
		}
	}
}
