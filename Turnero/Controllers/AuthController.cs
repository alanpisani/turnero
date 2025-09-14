using Microsoft.AspNetCore.Mvc;
using Turnero.Dto;
using Turnero.Service;

namespace Turnero.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController(AuthService service) : ControllerBase
	{
		private readonly AuthService _authService = service;

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto dto)
		{
			var response = await _authService.Conectarse(dto);

			if(!response.Exito) return Unauthorized(response.Mensaje);

			return Ok(response);
		}
	}
}
