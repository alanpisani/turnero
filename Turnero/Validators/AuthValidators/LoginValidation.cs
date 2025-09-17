using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Turnero.Dto;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Validators.AuthValidators
{
	public class LoginValidation: AbstractValidator<LoginDto>
	{
		private readonly IUnitOfWork _unitOfWork;

		public LoginValidation(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

			RuleFor(x => x.Email)
			.NotEmpty().WithMessage("Ingrese el correo electrónico")
			.MustAsync(async (email, CancellationToken) =>
					await unitOfWork.Usuarios.AnyUsuarioByEmail(email)).WithMessage("El correo electrónico no se encuentra registrado");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Ingrese la contraseña");
		}
	}
}
