using Microsoft.AspNetCore.Mvc;
using Turnero.Dto.Consultum;
using Turnero.Service;

namespace Turnero.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HistorialController(HistorialService service):ControllerBase
	{
		private readonly HistorialService _service = service;

		[HttpGet]
		public async Task<IActionResult> GetHistoriales()
		{
			var response = await _service.TraerTodosLosHistoriales();

			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> PostHistorial([FromBody]HistorialRequestDto dto)
		{
			var response = await _service.CrearHistorial(dto);

			return CreatedAtAction(nameof(PostHistorial), response);
		}
	}
}
