using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Turnero.Dto;
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

            return Ok(response);
        }
        [HttpGet("turnos/{dniPaciente}")]
        public async Task<IActionResult> GetTurnosByDniPaciente(int dniPaciente)
        {
            var response = await _service.TraerTurnosDelPacientePorDni(dniPaciente);

            return Ok(response);
        }


        [Authorize(Roles = "Paciente, Recepcionista, Admin")]
        [HttpPost]
        public async Task<IActionResult> PostTurno(TurnoDto turnoDto)
        {
			var response = await _service.SolicitarTurno(turnoDto);

			return CreatedAtAction(nameof(PostTurno), response);
		}

        [HttpPatch("{id}/cancelar")]
        public async Task<IActionResult> PatchCancelarTurno(int id)
        {
            var response = await _service.CancelarTurno(id);

            return Ok(response);
        }
    }
}
