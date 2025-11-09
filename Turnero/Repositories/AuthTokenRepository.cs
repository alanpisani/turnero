using Microsoft.EntityFrameworkCore;
using Turnero.Data;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class AuthTokenRepository(TurneroContext context): IAuthTokenRepository
	{
		private readonly TurneroContext _context = context;

		public async Task<List<AuthToken>> GetAuthTokensByUsuarioAndActivo(int idUsuario)
		{
			return await _context.AuthTokens
			.Where(t => t.IdUsuario == idUsuario && t.Activo == true)
			.ToListAsync();
		}

		public async Task<AuthToken?> FirstOrDefaultTokenActivo(string token)
		{
			return await _context.AuthTokens
					.FirstOrDefaultAsync(t => t.Token == token && t.Activo == true);
		}

		public async Task AddAuthToken(AuthToken token)
		{
			await _context.AuthTokens.AddAsync(token);
		}
	}
}
