using Microsoft.EntityFrameworkCore;
using Turnero.Common.Enums;
using Turnero.Data;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class TurnoRepository(TurneroContext context): ITurnoRepository
	{
		private readonly TurneroContext _context = context;

		public IQueryable<Turno> Query()
		{
			return _context.Turnos.AsQueryable();
		}

		public async Task AddTurno(Turno turno)
		{
			await _context.Turnos.AddAsync(turno);
		}

		public async Task<Turno?> FindOrDefaultTurno(int idTurno)
		{
			return await _context.Turnos
				.Include(p => p.IdPacienteNavigation)
				.Include(t => t.IdEspecialidadNavigation)
				.Include(p => p.IdProfesionalNavigation)
					.ThenInclude(p => p.ProfesionalEspecialidads)
						.ThenInclude(pe => pe.IdEspecialidadNavigation)
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
				.Where(t => t.EstadoTurno == EnumEstadoTurno.Solicitado.ToString())
				.Include(t => t.IdEspecialidadNavigation)
				.Include(p=> p.IdProfesionalNavigation)
					.ThenInclude(p=> p.ProfesionalEspecialidads)
						.ThenInclude(pe => pe.IdEspecialidadNavigation)
				.ToListAsync();
		}

		public async Task<List<Turno>?> GetTurnosByProfesionalAndFecha(int idProfesional, DateOnly fecha)
		{
			return await _context.Turnos
				.Where(t => t.IdProfesional == idProfesional && t.FechaTurno.Date == fecha.ToDateTime(TimeOnly.MinValue).Date)
				.ToListAsync();
		}
		public async Task<List<Turno>?> GetTurnosByProfesionalAndFechaDeHoy(int idProfesional)
		{
			return await _context.Turnos
				.Where(t => t.IdProfesional == idProfesional 
							&& t.FechaTurno.Date == DateTime.Today
							&& t.EstadoTurno !=EnumEstadoTurno.Cancelado.ToString()
				)
				.Include(t => t.IdEspecialidadNavigation)
				.Include(t => t.IdPacienteNavigation)
				.Include(p => p.IdProfesionalNavigation)
					.ThenInclude(p => p.ProfesionalEspecialidads)
						.ThenInclude(pe => pe.IdEspecialidadNavigation)
				.ToListAsync();
		}

		public void Actualizar(Turno turno)
		{
			_context.Turnos.Update(turno);
		}


		//Devuelve usuarios pero toca la tabla turno, por ende, va en TurnoRepository y se acabó
		public async Task<List<Usuario>> GetPacientesAtendidosPorProfesional(int profesionalId)
		{
			return await _context.Turnos //Tocá la tabla turno
				.Where(t => t.IdProfesional == profesionalId && t.EstadoTurno == EnumEstadoTurno.Atendido.ToString()) //Traeme los turnos que tengan a ese profesional y que sean turnos atendidos
				.Include(t => t.IdPacienteNavigation) //Inclui informacion de los pacientes relacionados a estos turnos (la tabla usuario)
				.Select(t => t.IdPacienteNavigation) //Map. Tranformame lo que tenemos hasta ahora en la tabla usuario traida
				.Distinct() //Eliminame los repetidos, por favor
				.ToListAsync(); //Listorti
		}

		public async Task<List<Turno>> GetAllTurnos()
		{
			return await _context.Turnos.ToListAsync();
		}
	}
}
