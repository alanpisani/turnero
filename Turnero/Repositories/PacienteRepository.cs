using Humanizer;
using Microsoft.EntityFrameworkCore;
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

		public async Task AddCoberturas(IEnumerable<CoberturaPaciente> coberturas)
		{
			foreach (var intermedia in coberturas) //Aplicamos el iterable yieldero
			{
				await _context.CoberturaPacientes.AddAsync(intermedia); //Se sube el registro de la/s tabla/s intermedia/s a la BD
			}
		}

		public async Task<bool> AnyPaciente(int idPaciente)
		{
			return await _context.Pacientes.AnyAsync(p => p.IdUsuario == idPaciente);
		}

		public async Task<List<int>> ToListAsyncIdsObrasSociales()
		{
			return await _context.CoberturaMedicas
				.Where(cm => cm.TipoCobertura == "Obra social")
				.Select(cm => cm.IdCoberturaMedica)
				.ToListAsync();
		}

		public async Task<List<Paciente>> ToListAsyncAllPacientes()
		{
			return await _context.Pacientes.ToListAsync();
		}
	}
}
