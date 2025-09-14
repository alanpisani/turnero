using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnero.Models;

namespace Turnero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoberturaMedicaController : ControllerBase
    {
        private readonly TurneroContext _context;

        public CoberturaMedicaController(TurneroContext context)
        {
            _context = context;
        }

        // GET: api/CoberturaMedica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoberturaMedica>>> GetCoberturaMedicas()
        {
            return await _context.CoberturaMedicas.ToListAsync();
        }

        // GET: api/CoberturaMedica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoberturaMedica>> GetCoberturaMedica(int id)
        {
            var coberturaMedica = await _context.CoberturaMedicas.FindAsync(id);

            if (coberturaMedica == null)
            {
                return NotFound();
            }

            return coberturaMedica;
        }

        // PUT: api/CoberturaMedica/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoberturaMedica(int id, CoberturaMedica coberturaMedica)
        {
            if (id != coberturaMedica.IdCoberturaMedica)
            {
                return BadRequest();
            }

            _context.Entry(coberturaMedica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoberturaMedicaExists(id))
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

        // POST: api/CoberturaMedica
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CoberturaMedica>> PostCoberturaMedica(CoberturaMedica coberturaMedica)
        {
            _context.CoberturaMedicas.Add(coberturaMedica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoberturaMedica", new { id = coberturaMedica.IdCoberturaMedica }, coberturaMedica);
        }

        // DELETE: api/CoberturaMedica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoberturaMedica(int id)
        {
            var coberturaMedica = await _context.CoberturaMedicas.FindAsync(id);
            if (coberturaMedica == null)
            {
                return NotFound();
            }

            _context.CoberturaMedicas.Remove(coberturaMedica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoberturaMedicaExists(int id)
        {
            return _context.CoberturaMedicas.Any(e => e.IdCoberturaMedica == id);
        }
    }
}
