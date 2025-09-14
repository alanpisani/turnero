using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Turnero.Common.Enums;
using Turnero.Models;

public class JwtService(IConfiguration configuration)
{
	private readonly IConfiguration _config = configuration;
	public string GenerateToken(Usuario usuario)
	{
		var claims = new List<Claim>
		{
			new(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
			new(ClaimTypes.Email, usuario.Email),
			new(ClaimTypes.Role, ((RolesUsuario) usuario.IdRol).ToString())
        };

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var expiry = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpiryInMinutes"]!));

		var token = new JwtSecurityToken(
			issuer: _config["Jwt:Issuer"],
			audience: _config["Jwt:Audience"],
			claims: claims,
			expires: expiry,
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}

 