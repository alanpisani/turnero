using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface IUsuarioRepository
	{
		public IQueryable<Usuario> Query();
		public Task<List<Usuario>> GetAll();	
		Task<bool> AnyUsuarioByEmail(string email);
		Task<bool> AnyUsuarioByDni(int dni);
		Task AddAsyncUsuario(Usuario usuario);
		Task<bool> AnyUsuarioByEmailAndPassword(string email, string password);
		Task<Usuario?> FirstOrDefaultUsuario(string email);
		Task<Usuario?> FirstOrDefaultUsuario(int id);
		Task<List<Usuario>> GetAllRecepcionistas();
		Task<bool> AnyRecepcionistaByDni(int dni);
	}
}
