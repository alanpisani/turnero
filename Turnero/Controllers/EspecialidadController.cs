using Microsoft.AspNetCore.Mvc;
using Turnero.Service;

namespace Turnero.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EspecialidadController(EspecialidadService service): ControllerBase
	{
		private readonly EspecialidadService _service = service;

		[HttpGet]
		public async Task<IActionResult> GetAllEspecialidades()
		{
			var response = await _service.MostrarTodasLasEspecialidades();

			if (!response.Exito) return BadRequest(response.Mensaje);

			return Ok(response.Cuerpo);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetEspecialidadById(int id)
		{
			var response = await _service.MostrarEspecialidadPorId(id);

			if (!response.Exito) return BadRequest(response.Mensaje);

			return Ok(response.Cuerpo);
		}
				
	}
}
