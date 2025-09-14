namespace Turnero.Repositories.Interfaces
{
	public interface IUnitOfWork: IAsyncDisposable
	{
		IUsuarioRepository Usuarios { get; }
		IPacienteRepository Pacientes { get; }
		IProfesionalRepository Profesionales { get; }
		ITurnoRepository Turnos { get; }
		IEspecialidadRepository Especialidades { get; }
		IHorarioLaboralRepository HorariosLaborales { get; }

		// Persistencia
		Task<int> CompleteAsync();

		// Transacciones
		Task BeginTransactionAsync();
		Task CommitAsync();
		Task RollbackAsync();
	}
}


