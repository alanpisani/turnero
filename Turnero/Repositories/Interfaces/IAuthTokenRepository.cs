using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface IAuthTokenRepository
	{
		Task<List<AuthToken>> GetAuthTokensByUsuarioAndActivo(int idUsuario);

		Task<AuthToken?> FirstOrDefaultTokenActivo(string token);
		Task AddAuthToken(AuthToken authToken);
	}
}
