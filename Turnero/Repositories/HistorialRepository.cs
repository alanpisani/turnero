using Microsoft.EntityFrameworkCore;
using Turnero.Data;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class HistorialRepository(TurneroContext context): IHistorialRepository
	{
		private readonly TurneroContext _context = context;

		public async Task<List<HistorialClinico>> GetAllHistoriales()
		{
			return await _context.HistorialClinicos
				.Include(c => c.IdTurnoNavigation)
				.ToListAsync();
		}
		public async Task<List<HistorialClinico>> GetHistorialesByPaciente(int idPaciente)
		{
			return await _context.HistorialClinicos
				.Where(c => c.IdTurnoNavigation.IdPaciente == idPaciente)
				.Include(c => c.IdTurnoNavigation)
				.ToListAsync();
		}

		public async Task AddHistorial(HistorialClinico historial)
		{
			await _context.HistorialClinicos.AddAsync(historial);
		}
	}
}
