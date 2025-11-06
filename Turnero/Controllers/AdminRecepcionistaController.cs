using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Turnero.Dto.Usuario;
using Turnero.Service;

namespace Turnero.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class Admin_RecepcionistaController(UsuarioService service) : ControllerBase
	{
		private readonly UsuarioService _service = service;

		[HttpGet()]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1)
		{
			var response = await _service.ConsultarUsuarios(pageNumber);

			return Ok(response);
		}

		[HttpPatch("usuario/{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> PatchUserState(int id, [FromQuery]bool state)
		{
			var result = await _service.CambiarEstadoUsuario(id, state);

			return Ok(result);
		}

		[HttpPost("registrar_recepcionista")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> PostRecepcionista(UsuarioRequestDto dto)
		{
			var response = await _service.RegistrarRecepcionista(dto);

			return CreatedAtAction(nameof(PostRecepcionista), response);
		}



		[HttpGet("recepcionista")]
		[Authorize(Roles ="Admin")]
		public async Task<IActionResult> GetRecepcionistas()
		{
			var response = await _service.MostrarTodosLosRecepcionistas();


			return Ok(response);
		}
	}
}
