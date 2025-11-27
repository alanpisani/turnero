using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface IHistorialRepository
	{
		Task<List<HistorialClinico>> GetAllHistoriales();
		Task<List<HistorialClinico>> GetHistorialesByPaciente(int idPaciente);
		Task AddHistorial(HistorialClinico historial);
		Task<bool> AnyHistorialByTurno(int idTurno);
	}
}
