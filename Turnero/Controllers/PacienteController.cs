using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnero.Dto;
using Turnero.Models;
using Turnero.Service;

namespace Turnero.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController(TurneroContext context, PacienteService service, TurnoService turnoService) : ControllerBase
    {
        private readonly TurneroContext _context = context;
        private readonly PacienteService _service = service;
        private readonly TurnoService _turnoService = turnoService;

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetPacientes()
        {
            var response = await _service.MostrarTodosLosPacientes();

            if (!response.Exito) return NotFound(response.Mensaje);

            return Ok(response.Cuerpo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPacienteById(int id)
        {
            var response = await _service.MostrarPacientePorId(id);

            if (!response.Exito)
            {
                return NotFound(response.Mensaje);
            }

            return Ok(response.Cuerpo);
        }

        // PUT: api/Paciente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, Paciente paciente)
        {
            if (id != paciente.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(paciente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Paciente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Paciente>>> PostPaciente(PacienteDto pacienteDto)
        {

			var response = await _service.RegistrarPaciente(pacienteDto);

			if (!response.Exito)
			{
				return BadRequest(response.Errores);
			}

			return CreatedAtAction(nameof(PostPaciente), response.Mensaje);
		}

        // DELETE: api/Paciente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.IdUsuario == id);
        }

        //Mis turnos

        [HttpGet("{idPaciente}/mis_turnos")]
        public async Task<IActionResult> GetTurnosByPaciente(int idPaciente)
        {
            var response = await _turnoService.TraerTurnosDelPaciente(idPaciente);

            if (!response.Exito)
            {
                return NotFound(response.Mensaje);
            }
            return Ok(response.Cuerpo);
        }


    }
}
