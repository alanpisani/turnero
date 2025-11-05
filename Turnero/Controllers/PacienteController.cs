using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Turnero.Dto.Paciente;
using Turnero.Dto.Usuario;
using Turnero.Service;

namespace Turnero.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController(PacienteService service, TurnoService turnoService) : ControllerBase
    {
        private readonly PacienteService _service = service;
        private readonly TurnoService _turnoService = turnoService;

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetPacientes()
        {
            var response = await _service.MostrarTodosLosPacientes();

            return Ok(response);
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPacienteById(int id)
        {
            var response = await _service.MostrarPacientePorId(id);

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> PostPaciente([FromBody]PacienteConTurnoRequestDto dto)
        {

			var response = await _service.RegistrarPacienteConTurno(dto);

			return CreatedAtAction(nameof(PostPaciente), response);
		}

        [HttpPost("rapido")]
        public async Task<IActionResult> PostPacienteRapido(UsuarioRapidoDto dto)
        {
            var response = await _service.RegistrarPacienteRapido(dto);

            return CreatedAtAction(nameof(PostPacienteRapido), response);
        }

        //Mis turnos

        [HttpGet("{idPaciente}/mis_turnos")]
        public async Task<IActionResult> GetTurnosByPaciente(int idPaciente)
        {
            var response = await _turnoService.TraerTurnosDelPaciente(idPaciente);

            return Ok(response);
        }


    }
}
