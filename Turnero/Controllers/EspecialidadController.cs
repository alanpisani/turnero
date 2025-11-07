using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Turnero.Dto.Especialidad;
using Turnero.Service;

namespace Turnero.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EspecialidadController(EspecialidadService service): ControllerBase
	{
		private readonly EspecialidadService _service = service;

		[HttpGet]
		public async Task<IActionResult> GetAllEspecialidadesPaginadas([FromQuery] int pageNumber) 
		{
			var response = await _service.MostrarTodasLasEspecialidadesPaginadas(pageNumber);


			return Ok(response);
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAllEspecialidades()
		{
			var response = await _service.MostrarTodasLasEspecialidades();

			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetEspecialidadById(int id)
		{
			var response = await _service.MostrarEspecialidadPorId(id);

			return Ok(response);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> PostEspecialidad(EspecialidadRequestDto dto)
		{
			var response = await _service.CrearEspecialidad(dto);

			return CreatedAtAction(nameof(PostEspecialidad), response);
		}
				
	}
}
