using Humanizer;
using Microsoft.EntityFrameworkCore;
using Turnero.Common.Enums;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class HorarioLaboralRepository(TurneroContext context): IHorarioLaboralRepository
	{
		private readonly TurneroContext _context = context;

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
