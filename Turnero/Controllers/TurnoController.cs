using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Turnero.Dto.TurnoDto;
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
        [HttpGet("profesional/{id}/turnos_de_hoy")]
        [Authorize(Roles = "Profesional")]
        public async Task<IActionResult> GetTurnosDeHoy(int id)
        {
            var response = await _service.TraerTurnosDeHoyPorProfesional(id);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostTurno(TurnoRequestDto turnoDto)
        {
			var response = await _service.SolicitarTurno(turnoDto);

			return CreatedAtAction(nameof(PostTurno), response);
		}

        [HttpPost("rapido")]
        public async Task<IActionResult> PostTurnoRapido(TurnoRapidoRequestDto turnoRapido)
        {
            var response = await _service.SolicitarTurnoRapido(turnoRapido);

            return Ok(response);
        } 

        [HttpPatch("{id}/cancelar")]
        public async Task<IActionResult> PatchCancelarTurno(int id, [FromBody] CancelarTurnoDto dto)
        {
            var response = await _service.CancelarTurno(id, dto);

            return Ok(response);
        }
    }
}
