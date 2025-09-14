using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Turnero.Dto;
using Turnero.Service;

namespace Turnero.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Admin_RecepcionistaController(UsuarioService service) : ControllerBase
	{
		private readonly UsuarioService _service = service;

		[HttpPost("/registrar_recepcionista")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> PostRecepcionista(UsuarioDto dto)
		{
			var response = await _service.RegistrarRecepcionista(dto);

			if(!response.Exito) return BadRequest(response.Errores);

			return CreatedAtAction(nameof(PostRecepcionista), response.Mensaje);
		}



		[HttpGet]
		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> GetRecepcionistas()
		{
			var response = await _service.MostrarTodosLosRecepcionistas();

			if (!response.Exito) return NotFound(response.Mensaje);

			return Ok(response.Cuerpo);
		}
	}
}
