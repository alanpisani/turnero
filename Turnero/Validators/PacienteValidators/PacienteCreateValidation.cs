using FluentValidation;
using Turnero.Dto.Paciente;
using Turnero.Repositories.Interfaces;
using Turnero.Validators.UsuarioValidators;

namespace Turnero.Validators.PacienteValidators
{
    public class PacienteCreateValidation: AbstractValidator<PacienteRequestDto>
	{
		public PacienteCreateValidation(IUnitOfWork unit) {

			Include(new UsuarioCreateValidation(unit));

		}
	}
}
