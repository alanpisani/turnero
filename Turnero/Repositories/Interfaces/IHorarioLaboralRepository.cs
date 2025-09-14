using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface IHorarioLaboralRepository
	{
		Task<HorarioLaboral?> FirstOrDefaultHorarioLaboral(int idProfesional, int diaSemana);
		Task<bool> AnyHoraDentroDeFranja(int idProfesional, TimeOnly horaIngresada);
	}
}
