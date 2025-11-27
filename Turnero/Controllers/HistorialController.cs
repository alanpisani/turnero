using Microsoft.AspNetCore.Mvc;
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
	}
}
