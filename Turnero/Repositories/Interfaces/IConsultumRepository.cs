using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface IConsultumRepository
	{
		Task<List<Consultum>> GetConsultumsByPaciente(int idPaciente);
		Task AddConsultum(Consultum consultum);
	}
}
