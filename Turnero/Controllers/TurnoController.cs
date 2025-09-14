using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Turnero.Dto;
using Turnero.Models;
using Turnero.Service;

namespace Turnero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController(TurnoService service) : ControllerBase
    {
        private readonly TurnoService _service = service;

        [HttpGet("{idPaciente}")]
        public async Task<IActionResult> GetTurnosByPaciente(int idPaciente)
        {
            var response = await _service.TraerTurnosDelPaciente(idPaciente);

            if (!response.Exito)
            {
                return NotFound();
            }

            return Ok(response.Cuerpo);
        }


        [Authorize(Roles = "Paciente, Recepcionista")]
        [HttpPost]
        public async Task<IActionResult> PostTurno(TurnoDto turnoDto)
        {
			var response = await _service.SolicitarTurno(turnoDto);

			if (!response.Exito)
			{
				return BadRequest(response.Errores);
			}
			return CreatedAtAction(nameof(PostTurno), response.Cuerpo);
		}

        [HttpPatch("{id}/cancelar")]
        public async Task<IActionResult> PatchCancelarTurno(int id)
        {
            var response = await _service.CancelarTurno(id);

            if (!response.Exito)
            {
                return BadRequest(response);
            }

            return Ok(response.Mensaje);
        }
    }
}
