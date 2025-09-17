using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface ICoberturaMedicaRepository
	{
		Task<List<CoberturaMedica>> GetCoberturas();
		Task<CoberturaMedica?> GetCoberturaById(int idCobertura);
		Task<bool> AnyCobertura(string nombre);
		Task CreateCoberturaMedica(CoberturaMedica cobertura);
	}
}
