using Humanizer;
using Microsoft.EntityFrameworkCore;
using Turnero.Controllers;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class TurnoRepository(TurneroContext context):ITurnoRepository
	{
		private readonly TurneroContext _context = context;

		public async Task AddTurno(Turno turno)
		{
			await _context.Turnos.AddAsync(turno);
		}

		public async Task<Turno?> FindOrDefaultTurno(int idTurno)
		{
			return await _context.Turnos
				.FirstOrDefaultAsync(t => t.IdTurno == idTurno);
		}

		public async Task<bool> AnyTurnoOcupado(int idProfesional, DateTime fechaYHoraIngresada)
		{
			return await _context.Turnos
				.Where(t => t.IdProfesional == idProfesional)
				.AnyAsync(t => t.FechaTurno == fechaYHoraIngresada);
		}

		public Task<List<Turno>> GetTurnosByPaciente(int idPaciente)
		{
			return _context.Turnos
				.Where(t => t.IdPaciente == idPaciente)
				.ToListAsync();
		}

		public void Actualizar(Turno turno)
		{
			_context.Turnos.Update(turno);
		}
	}
}
