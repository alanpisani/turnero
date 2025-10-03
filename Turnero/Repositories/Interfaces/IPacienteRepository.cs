using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface IPacienteRepository
	{
		Task AddPaciente(Paciente paciente);
		Task AddCoberturas(IEnumerable<CoberturaPaciente> coberturas);
		Task<bool> AnyPaciente(int idPaciente);
		Task<List<int>> ToListAsyncIdsObrasSociales();
        Task<List<Paciente>> ToListAsyncAllPacientes();
		Task<Paciente?> GetPacienteById(int id);
	}
}
