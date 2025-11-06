using Turnero.Dto.Especialidad;
using Turnero.Models;

namespace Turnero.Mappers
{
	public class EspecialidadMapper
	{
		public static EspecialidadResponseDto toResponseDto(Especialidad especialidad)
		{
			return new EspecialidadResponseDto
			{
				IdEspecialidad = especialidad.IdEspecialidad,
				NombreEspecialidad = especialidad.NombreEspecialidad,
				IsActive = especialidad.IsActive
			};
		}
	}
}
