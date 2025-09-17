using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Common.Helpers.ProfesionalValidatorHelper
{
	/// <summary>
	/// Helper que me ayuda a modularizar validaciones para la lógica del negocio en cuanto a las ESPECIALIDADES DEL PROFESIONAL A CREAR
	/// </summary>
	public static class ProfesionalEspecialidadValidatorHelper
	{
		public static bool SinEspecialidadesRepetidas(List<int> especialidades)
		{
			var especialidadesDuplicadas = especialidades
				.GroupBy(g => g)
				.Where(g => g.Count() > 1)
				.Select(g => g.Key)
				.ToList();

			return especialidadesDuplicadas.Count == 0;
		}

		public static async Task<bool> EspecialidadExiste(List<int> especialidades, IUnitOfWork unitOfWork)
		{

			var todasLasEspecialidades = await unitOfWork.Especialidades.ToListAsyncEspecialidades();

			foreach (var especialidad in especialidades)
			{
				var existeEspecialidad = todasLasEspecialidades.Any(e => e.IdEspecialidad == especialidad);
				if (!existeEspecialidad)
				{
					return false;
				}
			}

			return true;
		}
	}
}
