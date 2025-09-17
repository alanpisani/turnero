using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnero.Models;
using Turnero.Service;

namespace Turnero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoberturaMedicaController(CoberturaMedicaService service, TurneroContext context) : ControllerBase
    {
        private readonly TurneroContext _context = context;
        private readonly CoberturaMedicaService _service = service;

        // GET: api/CoberturaMedica
        [HttpGet]
        public async Task<IActionResult> GetCoberturasMedicas()
        {
            var response = await _service.MostrarTodasLasCoberturas();
            
            if(!response.Exito) return BadRequest(response.Mensaje);

            return Ok(response.Cuerpo);
        }

        // GET: api/CoberturaMedica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoberturaMedica>> GetCoberturaMedica(int id)
        {
            var response = await _service.MostrarCoberturaPorId(id);

            if (!response.Exito)
            {
                return NotFound(response.Mensaje);
            }

            return response.Cuerpo!;
        }
    }
}
