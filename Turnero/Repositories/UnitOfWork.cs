using Microsoft.EntityFrameworkCore.Storage;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class UnitOfWork: IUnitOfWork
	{
		private readonly TurneroContext _context;

		public IUsuarioRepository Usuarios { get; }
		public IPacienteRepository Pacientes { get; }
		public IProfesionalRepository Profesionales { get; }
		public ITurnoRepository Turnos { get; }
		public IEspecialidadRepository Especialidades { get; }
		public IHorarioLaboralRepository HorariosLaborales { get; }

		public IAuthTokenRepository AuthTokens { get; }


		private IDbContextTransaction? _transaction;

		public UnitOfWork(TurneroContext context, 
			IPacienteRepository pacientes, 
			IProfesionalRepository profesionales, 
			ITurnoRepository turnos, 
			IEspecialidadRepository especialidades, 
			IHorarioLaboralRepository horariosLaborales, 
			IUsuarioRepository usuarioRepository, IAuthTokenRepository authTokenRepository)
		{
			_context = context;
			Pacientes = pacientes;
			Profesionales = profesionales;
			Turnos = turnos;
			Especialidades = especialidades;
			HorariosLaborales = horariosLaborales;
			Usuarios = usuarioRepository;
			AuthTokens = authTokenRepository;
		}

		public async Task<int> CompleteAsync()
		{
			return await _context.SaveChangesAsync();
		}
		public async Task BeginTransactionAsync()
		{
			_transaction = await _context.Database.BeginTransactionAsync();
		}

		public async Task CommitAsync()
		{
			if (_transaction != null)
			{
				await _transaction.CommitAsync();
				await _transaction.DisposeAsync();
				_transaction = null;
			}
		}

		public async Task RollbackAsync()
		{
			if (_transaction != null)
			{
				await _transaction.RollbackAsync();
				await _transaction.DisposeAsync();
				_transaction = null;
			}
		}

		public async ValueTask DisposeAsync()
		{
			if (_transaction != null)
			{
				await _transaction.RollbackAsync();
				await _transaction.DisposeAsync();
				_transaction = null;
			}

			if (_context != null)
			{
				await _context.DisposeAsync();
			}
		}
	}
}
