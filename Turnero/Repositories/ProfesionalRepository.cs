using System.Collections.Generic;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class ProfesionalRepository(TurneroContext context): IProfesionalRepository
	{
		private readonly TurneroContext _context = context;

		/**  ADD  **/
		public async Task AddProfesional(Profesional profesional)
		{
			await _context.Profesionals.AddAsync(profesional); //Directo a la BD
		}

		public async Task AddEspecialidades(IEnumerable<ProfesionalEspecialidad> asignaciones)
		{
			foreach (var asignacion in asignaciones) //Subimos las especialidades del profesional a la BD
			{
				await _context.ProfesionalEspecialidads.AddAsync(asignacion);
			}
		}

		public async Task AddHorarios(IEnumerable<HorarioLaboral> asignaciones)
		{
			foreach (var asignacion in asignaciones) //Subimos sus horarios Laborales a la BD
			{
				await _context.HorarioLaborals.AddAsync(asignacion);
			}
		}

		/*******/

		/**  ANY  **/
		public async Task<bool> AnyProfesional(int idProfesional)
		{
			return await _context.Profesionals.AnyAsync(p => p.IdUsuario == idProfesional);
		}

		public async Task<bool> AnyProfesionalWithThatSpeciality(int idProfesional, int idEspecialidad)
		{
			return await _context.ProfesionalEspecialidads
				.AnyAsync(inter =>
					inter.IdProfesional == idProfesional &&
					inter.IdEspecialidad == idEspecialidad);
		}

		public async Task<bool> AnyProfesionalByMatricula(int matricula)
		{
			return await _context.Profesionals
				.AnyAsync(p => p.Matricula == matricula);
		}

		/*******/

		/***  GET  ***/

		public async Task<List<Profesional>> GetAllProfesionals()
		{
			return await _context.Profesionals
				.Include(p=> p.IdUsuarioNavigation)
				.ToListAsync();
		}

		public async Task<Profesional?> GetProfesionalById(int id)
		{
			return await _context.Profesionals
				.Where(p=> p.IdUsuario == id)
				.Include(p => p.IdUsuarioNavigation)
				.FirstOrDefaultAsync();
				
		}

		public async Task<List<Profesional>> GetProfesionalesByEspecialidad(int idEspecialidad)
		{
			return await _context.Profesionals
				.Where(p => p.ProfesionalEspecialidads
					.Any(pe => pe.IdEspecialidad == idEspecialidad))
			.ToListAsync();

		}
	}
}
