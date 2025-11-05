using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Common.Helpers.TurnoValidatorHelper
{
	public static class TurnoDatosValidationHelper
	{
		public static async Task<bool> PacienteExiste(int? idPaciente, IUnitOfWork uow)
		{
			return await uow.Pacientes.AnyPaciente(idPaciente);
		}

		public static async Task<bool> ProfesionalExiste(int idProfesional, IUnitOfWork uow)
		{
			return await uow.Profesionales.AnyProfesional(idProfesional);
		}

		public static async Task<bool> EspecialidadExiste(int idEspecialidad, IUnitOfWork uow)
		{
			return await uow.Especialidades.AnyEspecialidad(idEspecialidad);
		}
	}
}
