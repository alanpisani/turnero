using Microsoft.EntityFrameworkCore;
using Turnero.Data;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class PacienteRepository(TurneroContext context): IPacienteRepository
	{
		private readonly TurneroContext _context = context;

		public async Task AddPaciente(Paciente paciente)
		{
			await _context.Pacientes.AddAsync(paciente);
		}
		public async Task<bool> AnyPaciente(int? idPaciente)
		{
			return await _context.Pacientes.AnyAsync(p => p.IdUsuario == idPaciente); //Existe algun paciente con ese id o no
		}
		public async Task<Paciente?> GetPacienteWithDni(int dni)
		{
			return await _context.Pacientes
				.Where(p=> p.IdUsuarioNavigation.Dni == dni)
				.FirstOrDefaultAsync();
		}
		public async Task<List<Paciente>> ToListAsyncAllPacientes()
		{
			return await _context.Pacientes
				.Include(p => p.IdUsuarioNavigation)
				.ToListAsync();
		}

		public async Task<Paciente?> GetPacienteById(int id) {
			return await _context.Pacientes
				.Include(p => p.IdUsuarioNavigation)
				.FirstOrDefaultAsync(p => p.IdUsuario == id);
		}

		public async Task<Paciente?> GetPacienteByDni(int dniPaciente)
		{
			return await _context.Pacientes
				.Include(p => p.IdUsuarioNavigation)
				.FirstOrDefaultAsync(p => p.IdUsuarioNavigation.Dni == dniPaciente);
		}
	}
}
