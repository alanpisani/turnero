using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface IProfesionalRepository
	{
		Task AddProfesional(Profesional profesional);
		Task AddEspecialidades(IEnumerable<ProfesionalEspecialidad> asignaciones);
		Task AddHorarios(IEnumerable<HorarioLaboral> asignaciones);
		Task<bool> AnyProfesional(int idProfesional);
		Task<bool> AnyProfesionalWithThatSpeciality(int idProfesional, int idEspecialidad);
		Task<bool> AnyProfesionalByMatricula(int matricula);

		Task<List<Profesional>?> GetProfesionalesByEspecialidad(int idEspecialidad);
	}
}
