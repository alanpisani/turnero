using Microsoft.EntityFrameworkCore;
using Turnero.Data;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class ConsultumRepository(TurneroContext context): IConsultumRepository
	{
		private readonly TurneroContext _context = context;

		public async Task<List<Consultum>> GetConsultumsByPaciente(int idPaciente)
		{
			return await _context.Consulta
				.Where(c => c.IdPaciente == idPaciente)
				.ToListAsync();
		}

		public async Task AddConsultum(Consultum consultum)
		{
			await _context.Consulta.AddAsync(consultum);
		}
	}
}
