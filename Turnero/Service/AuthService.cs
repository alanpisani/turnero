using Microsoft.AspNetCore.Identity;
using Turnero.Dto;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Service
{
	public class AuthService(IUnitOfWork unit, JwtService jwt)
	{
		private readonly IUnitOfWork _unitOfWork = unit;
		private readonly JwtService _jwtService = jwt;
		private readonly PasswordHasher<Usuario> _passwordHasher = new();

		public async Task<ServiceResponse<string>> Conectarse(LoginDto dto)
		{
			var usuario = await _unitOfWork.Usuarios.FirstOrDefaultUsuario(dto.Email);

			if (usuario == null)
			{
				return new ServiceResponse<string>
				{
					Mensaje = "El correo electrónico no se encuentra registrado"
				};
			}

			var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Contrasenia, dto.Password);

			if (result == PasswordVerificationResult.Failed) {

				return new ServiceResponse<string>
				{
					Mensaje = "La contraseña es incorrecta"
				};
			}

			var token = _jwtService.GenerateToken(usuario);

			return new ServiceResponse<string> { 
				Exito = true,
				Mensaje = $"Bienvenido, {usuario.Nombre}!",
				Cuerpo = token,
			};
		}
	}
}
