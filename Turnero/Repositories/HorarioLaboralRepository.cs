using Microsoft.EntityFrameworkCore;
using Turnero.Data;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class HorarioLaboralRepository(TurneroContext context): IHorarioLaboralRepository
	{
		private readonly TurneroContext _context = context;

		public async Task<List<HorarioLaboral>> GetAllByProfesional(int idProfesional)
		{
			return await _context.HorarioLaborals
				.Where(h=> h.IdProfesional == idProfesional)
				.ToListAsync();
		}
		public async Task<HorarioLaboral?> FirstOrDefaultHorarioLaboral(int idProfesional, int diaSemana)
		{
			return await _context.HorarioLaborals
				.FirstOrDefaultAsync(e => e.IdProfesional == idProfesional && e.DiaSemana == diaSemana);
		}
		public async Task<bool> AnyHoraDentroDeFranja(int idProfesional, TimeOnly horaIngresada)
		{
			return await _context.HorarioLaborals
				.Where(hl => hl.IdProfesional == idProfesional)
				.AnyAsync(hl => horaIngresada >= hl.HoraInicio && horaIngresada <= hl.HoraFin);
		}
	}
}
