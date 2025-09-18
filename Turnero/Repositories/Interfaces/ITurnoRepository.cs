using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface ITurnoRepository
	{
		Task AddTurno(Turno turno);
		Task<Turno?> FindOrDefaultTurno(int idTurno);
		Task<bool> AnyTurnoOcupado(int idProfesional, DateTime fechaYHoraIngresada);
		Task<List<Turno>> GetTurnosByPaciente(int idPaciente);
		Task<List<Turno>?> GetTurnosByProfesionalAndFecha(int idProfesional, DateOnly fecha);
		void Actualizar(Turno turno);
	}
}
