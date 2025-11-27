using Microsoft.EntityFrameworkCore.Storage;
using Turnero.Data;
using Turnero.Repositories.Interfaces;

namespace Turnero.Repositories
{
	public class UnitOfWork(TurneroContext context,
		IPacienteRepository pacientes,
		IProfesionalRepository profesionales,
		ITurnoRepository turnos,
		IEspecialidadRepository especialidades,
		IHorarioLaboralRepository horariosLaborales,
		IUsuarioRepository usuarioRepository, IAuthTokenRepository authTokenRepository, IHistorialRepository historialesClinicos) : IUnitOfWork
	{
		private readonly TurneroContext _context = context;

		public IUsuarioRepository Usuarios { get; } = usuarioRepository;
		public IPacienteRepository Pacientes { get; } = pacientes;
		public IProfesionalRepository Profesionales { get; } = profesionales;
		public ITurnoRepository Turnos { get; } = turnos;
		public IEspecialidadRepository Especialidades { get; } = especialidades;
		public IHorarioLaboralRepository HorariosLaborales { get; } = horariosLaborales;

		public IAuthTokenRepository AuthTokens { get; } = authTokenRepository;
		public IHistorialRepository HistorialesClinicos { get; } = historialesClinicos;

		private IDbContextTransaction? _transaction;

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
