using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Turnero.Common.Enums;
using Turnero.Models;

public class JwtService(IConfiguration configuration) //EL GENERADOR DE TOKENS POR EXCELENCIA
{
	private readonly IConfiguration _config = configuration;
	public string GenerateToken(Usuario usuario)
	{
		var claims = new List<Claim> //Lista de datos representativos del usuario que va a loggear
		{
			new("id", usuario.IdUsuario.ToString()),
			new("email", usuario.Email),
			new("rol", usuario.Rol),
			new("nombre", usuario.Nombre),
			new("dni", usuario.Dni.ToString())
		};
		Console.WriteLine(usuario.Rol);

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		//La duracion del token. La fecha de expiracion se podria decir
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

 