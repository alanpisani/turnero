using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

		public async Task<ResponseDto<string?>> Conectarse(LoginDto dto)
		{
			var usuario = await _unitOfWork.Usuarios.FirstOrDefaultUsuario(dto.Email);

			var result = _passwordHasher.VerifyHashedPassword(usuario!, usuario!.Contrasenia, dto.Password);

			if (result == PasswordVerificationResult.Failed) throw new UnauthorizedAccessException("La contraseña es incorrecta");

			var tokenPrevio = await _unitOfWork.AuthTokens.GetAuthTokensByUsuarioAndActivo(usuario.IdUsuario);

			foreach (var t in tokenPrevio)
			{
				t.Activo = false;
			}

			// Generar nuevo token
			var jwt = _jwtService.GenerateToken(usuario);

			var authToken = new AuthToken
			{
				IdUsuario = usuario.IdUsuario,
				Token = jwt,
				Activo = true,
				Expiracion = DateTime.UtcNow.AddMinutes(1)
			};

			await _unitOfWork.AuthTokens.AddAuthToken(authToken);
			await _unitOfWork.CompleteAsync();

			return new ResponseDto<string?>
			{
				Success = true,
				Message = "Token generado con éxito",
				Data = jwt
			};
		}
	}
}
