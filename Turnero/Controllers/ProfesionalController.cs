using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnero.Dto.Profesional;
using Turnero.Models;
using Turnero.Service;

namespace Turnero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionalController(ProfesionalService service) : ControllerBase
    {
        private readonly ProfesionalService _service = service;

		// GET: api/Profesional
		[HttpGet]
        public async Task<IActionResult> GetProfesionals()
        {
            var response = await _service.MostrarProfesionales();

            return Ok(response);
        }  

        // GET: api/Profesional/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfesional(int id)
        {
            var profesional = await _service.MostrarProfesionalPorId(id);

            return Ok(profesional);
        }

        [HttpGet("especialidad/{idEspecialidad}")]
        public async Task<IActionResult> GetProfesionalesByEspecialidad(int idEspecialidad)
        {
            var response = await _service.MostrarProfesionalesPorEspecialidad(idEspecialidad);

            return Ok(response);
        }

        [HttpGet("{idProfesional}/disponibilidad")]
        public async Task<IActionResult> GetDiasDisponibles(int idProfesional)
        {
            var response = await _service.GetDiasDisponiblesProfesional(idProfesional);

            return Ok(response);
        }

        [HttpGet("{id}/franjas")]
        public async Task<IActionResult> GetFranjas(int id, [FromQuery] string fecha)
        {
			var response = await _service.GetFranjaHoraria(id, fecha);

            return Ok(response);
		}

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostProfesional(ProfesionalRequestDto profesionalDto)
        {
			var response = await _service.RegistrarProfesional(profesionalDto);

			return CreatedAtAction(nameof(PostProfesional), response);
			
        }
    }
}
