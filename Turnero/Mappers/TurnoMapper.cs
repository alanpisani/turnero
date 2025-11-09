using Turnero.Common.Enums;
using Turnero.Dto.TurnoDto;
using Turnero.Models;

namespace Turnero.Mappers
{
    public class TurnoMapper
	{
		public static Turno DeTurnoDtoATurno(TurnoRequestDto dto)
		{
			var dia = DateOnly.Parse(dto.Dia);
			var hora = TimeOnly.Parse(dto.Hora);

			return new Turno
			{
				IdPaciente= dto.IdPaciente ?? 0,
				IdProfesional= dto.IdProfesional,
				IdEspecialidad= dto.IdEspecialidad,
				EstadoTurno= EnumEstadoTurno.Solicitado.ToString(),
				FechaTurno= dia.ToDateTime(hora)
			};
		}

		public static TurnoResponseDto DeTurnoADto(Turno turno)
		{
			return new TurnoResponseDto
			{
				IdTurno = turno.IdTurno,
				Especialidad= turno.IdEspecialidadNavigation.NombreEspecialidad,
				Fecha= turno.FechaTurno.ToString(),
				EstadoTurno = turno.EstadoTurno
			};
		}

		public static TurnoRequestDto DeRapidoRequestARequestDto(TurnoRapidoRequestDto dto, int idPaciente)
		{
			return new TurnoRequestDto
			{
				IdPaciente = idPaciente,
				IdEspecialidad = dto.IdEspecialidad,
				IdProfesional = dto.IdProfesional,
				Dia = dto.Dia,
				Hora = dto.Hora,
			};
		}

		public static TurnsOfTheDayDto ToOfTheDayDto(Turno turno)
		{
			return new TurnsOfTheDayDto
			{
				IdPaciente = turno.IdPaciente,
				IdTurno = turno.IdTurno,
				NombrePaciente = turno.IdPacienteNavigation.Nombre,
				Especialidad = turno.IdEspecialidadNavigation.NombreEspecialidad,
				Hora = TimeOnly.FromDateTime(turno.FechaTurno).ToString(),
			};
		}
	}
}
