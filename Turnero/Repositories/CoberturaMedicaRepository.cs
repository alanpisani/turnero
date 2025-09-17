using Microsoft.EntityFrameworkCore;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class CoberturaMedicaRepository(TurneroContext context): ICoberturaMedicaRepository
	{
		private readonly TurneroContext _context = context;
		public async Task<List<CoberturaMedica>> GetCoberturas()
		{
			return await _context.CoberturaMedicas
				.ToListAsync();
		}
		public async Task<CoberturaMedica?> GetCoberturaById(int idCobertura)
		{
			return await _context.CoberturaMedicas
				.FirstOrDefaultAsync(c => c.IdCoberturaMedica == idCobertura);
		}

		public async Task CreateCoberturaMedica(CoberturaMedica cobertura)
		{
			await _context.CoberturaMedicas.AddAsync(cobertura);
		}

		public async Task<bool> AnyCobertura(string nombre)
		{
			return await _context.CoberturaMedicas
				.AnyAsync(c => c.Nombre == nombre);
		}


	}
}
