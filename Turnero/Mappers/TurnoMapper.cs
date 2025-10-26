using Turnero.Common.Enums;
using Turnero.Dto;
using Turnero.Models;

namespace Turnero.Mappers
{
	public class TurnoMapper
	{
		public static Turno DeTurnoDtoATurno(TurnoDto dto)
		{
			var dia = DateOnly.Parse(dto.Dia);
			var hora = TimeOnly.Parse(dto.Hora);

			return new Turno
			{
				IdPaciente= dto.IdPaciente,
				IdProfesional= dto.IdProfesional,
				IdEstadoTurno= (int) EnumEstadoTurno.Solicitado,
				FechaTurno= dia.ToDateTime(hora)
			};
		}
	}
}
