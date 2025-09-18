using Microsoft.EntityFrameworkCore;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class EspecialidadRepository(TurneroContext context): IEspecialidadRepository
	{
		private readonly TurneroContext _context = context;

		public async Task<bool> AnyEspecialidad(int idEspecialidad)
		{
			return await _context.Especialidads
				.AnyAsync(e => e.IdEspecialidad == idEspecialidad);
		}
		public async Task<List<Especialidad>> ToListAsyncEspecialidades()
		{
			return await _context.Especialidads.ToListAsync();
		}
		public async Task<Especialidad?> FirstOrDefaultEspecialidadById(int id)
		{
			return await _context.Especialidads.FirstOrDefaultAsync(
				e => e.IdEspecialidad == id
				);
		} 
	}
}