using Microsoft.EntityFrameworkCore;
using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface IEspecialidadRepository
	{
		IQueryable<Especialidad> Query();
		Task<bool> AnyEspecialidad(int idEspecialidad);
		Task<bool> AnyEspecialidad(string nombre);
		Task<List<Especialidad>> ToListAsyncEspecialidades();
		Task<Especialidad?> FirstOrDefaultEspecialidadById(int id);
		Task AddEspecialidad(Especialidad especialidad);
	}
}
