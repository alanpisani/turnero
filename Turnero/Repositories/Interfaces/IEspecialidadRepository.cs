using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface IEspecialidadRepository
	{
		Task<bool> AnyEspecialidad(int idEspecialidad);
		Task<List<Especialidad>> ToListAsyncEspecialidades();
	}
}
