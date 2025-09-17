using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnero.Dto;
using Turnero.Models;
using Turnero.Service;

namespace Turnero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionalController : ControllerBase
    {
        private readonly TurneroContext _context;
        private readonly ProfesionalService _service;

        public ProfesionalController(TurneroContext context, ProfesionalService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Profesional
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesional>>> GetProfesionals()
        {
            return await _context.Profesionals.ToListAsync();
        }  

        // GET: api/Profesional/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesional>> GetProfesional(int id)
        {
            var profesional = await _context.Profesionals.FindAsync(id);

            if (profesional == null)
            {
                return NotFound();
            }

            return profesional;
        }

        [HttpGet("{id}/franjas")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<string>>>> GetFranjas(int id, [FromQuery] string fecha)
        {
			var response = await _service.GetFranjaHoraria(id, fecha);

            if (!response.Exito) return NotFound(response);

            return response;
		}

        // PUT: api/Profesional/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesional(int id, Profesional profesional)
        {
            if (id != profesional.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(profesional).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesionalExists(id))
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostProfesional(ProfesionalDto profesionalDto)
        {
			var response = await _service.RegistrarProfesional(profesionalDto);

            if (!response.Exito)
            {
                return BadRequest(response.Mensaje);
            }
			return CreatedAtAction(nameof(PostProfesional), response.Mensaje);
			
        }

        // DELETE: api/Profesional/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesional(int id)
        {
            var profesional = await _context.Profesionals.FindAsync(id);
            if (profesional == null)
            {
                return NotFound();
            }

            _context.Profesionals.Remove(profesional);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesionalExists(int id)
        {
            return _context.Profesionals.Any(e => e.IdUsuario == id);
        }
    }
}
