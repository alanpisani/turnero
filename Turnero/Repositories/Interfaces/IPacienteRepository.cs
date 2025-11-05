using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface IPacienteRepository
	{
		Task AddPaciente(Paciente paciente);
		Task<bool> AnyPaciente(int? idPaciente);
		Task<Paciente?> GetPacienteWithDni(int dni);
		Task<Paciente?> GetPacienteByDni(int dni);
        Task<List<Paciente>> ToListAsyncAllPacientes();
		Task<Paciente?> GetPacienteById(int id);
	}
}
