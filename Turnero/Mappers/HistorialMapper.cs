using Turnero.Dto.Consultum;
using Turnero.Models;

namespace Turnero.Mappers
{
	public static class HistorialMapper
	{
		public static HistorialResponseDto ToDto(HistorialClinico historial)
		{
			return new HistorialResponseDto
			{
				IdHistorial = historial.IdHistorial,
				FechaConsulta = historial.IdTurnoNavigation.FechaCreacion.ToString(),
				NombrePaciente = historial.IdTurnoNavigation.IdPacienteNavigation.Nombre + " " +historial.IdTurnoNavigation.IdPacienteNavigation.Apellido,
				Diagnostico = historial.Diagnostico,
				Tratamiento = historial.Tratamiento,
				Observaciones = historial.Observaciones == "" || historial.Observaciones == null 
					? "Sin observaciones" 
					: historial.Observaciones
			};
		}

		public static HistorialClinico ToModel(HistorialRequestDto dto)
		{
			return new HistorialClinico { 
				IdTurno = dto.IdTurno,
				Tratamiento = dto.Tratamiento,
				Diagnostico= dto.Diagnostico,
				Observaciones= dto.Observaciones,
			};
		}
	}
}
