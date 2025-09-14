using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface ITurnoRepository
	{
		Task AddTurno(Turno turno);
		Task<Turno?> FindOrDefaultTurno(int idTurno);
		Task<bool> AnyTurnoOcupado(int idProfesional, DateTime fechaYHoraIngresada);
		Task<List<Turno>> GetTurnosByPaciente(int idPaciente);
		void Actualizar(Turno turno);
	}
}
