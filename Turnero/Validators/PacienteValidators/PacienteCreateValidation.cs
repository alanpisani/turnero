using FluentValidation;
using Turnero.Dto;
using Turnero.Repositories.Interfaces;
using Turnero.Validators.UsuarioValidators;

namespace Turnero.Validators.PacienteValidators
{
	public class PacienteCreateValidation: AbstractValidator<PacienteDto>
	{
		public PacienteCreateValidation(IUnitOfWork unit) {

			Include(new UsuarioCreateValidation(unit));

		}
	}
}
