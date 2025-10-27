﻿using Turnero.Models;

namespace Turnero.Repositories.Interfaces
{
	public interface IUsuarioRepository
	{
		Task<bool> AnyUsuarioByEmail(string email);
		Task<bool> AnyUsuarioByDni(int dni);
		Task AddAsyncUsuario(Usuario usuario);
		Task<bool> AnyUsuarioByEmailAndPassword(string email, string password);
		Task<Usuario?> FirstOrDefaultUsuario(string email);
		
		Task<List<Usuario>> GetAllRecepcionistas();
	}
}
