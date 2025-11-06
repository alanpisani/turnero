using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Turnero.Common.Enums;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class UsuarioRepository(TurneroContext context):IUsuarioRepository
	{
		private readonly TurneroContext _context = context;

		public IQueryable<Usuario> Query()
		{
			return _context.Usuarios.AsQueryable();
		}

		public async Task<List<Usuario>> GetAll()
		{
			return await _context.Usuarios.ToListAsync();
		}

		public async Task<bool> AnyUsuarioByEmail(string email)
		{
			return await _context.Usuarios
				.AnyAsync(u => u.Email == email);
		}
		public async Task<bool> AnyUsuarioByDni(int dni)
		{
			return await _context.Usuarios
				.AnyAsync(U=> U.Dni == dni);
		}
		public async Task AddAsyncUsuario(Usuario usuario)
		{
			await _context.Usuarios.AddAsync(usuario);
		}

		public async Task<bool> AnyUsuarioByEmailAndPassword(string email, string password)
		{
			return await _context.Usuarios
				.AnyAsync(u => u.Email == email && u.Password == password);
		}

		public async Task<Usuario?> FirstOrDefaultUsuario(string email)
		{
			return await _context.Usuarios
				.FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task<Usuario?> FirstOrDefaultUsuario(int id) //Sobreescritura del método de arriba
		{
			return await _context.Usuarios
				.FirstOrDefaultAsync(u => u.IdUsuario == id);
		}

		//RECEPCIONISTA

		public async Task<List<Usuario>> GetAllRecepcionistas()
		{
			return await _context.Usuarios
				.Where(u => u.Rol == RolesUsuario.Recepcionista.ToString())
				.ToListAsync();
		}

		public async Task<bool> AnyRecepcionistaByDni(int dni)
		{
			return await _context.Usuarios
				.Where(u=> u.Rol == RolesUsuario.Recepcionista.ToString())
				.AnyAsync(u=> u.Dni == dni);
		}
	}
}
