using Microsoft.EntityFrameworkCore;
using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface IEspecialidadRepository
	{
		public IQueryable<Especialidad> Query();
		Task<bool> AnyEspecialidad(int idEspecialidad);
		Task<List<Especialidad>> ToListAsyncEspecialidades();
		Task<Especialidad?> FirstOrDefaultEspecialidadById(int id);
	}
}
