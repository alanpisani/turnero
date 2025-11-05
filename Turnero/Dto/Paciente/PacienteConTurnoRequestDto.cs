using Turnero.Dto.TurnoDto;

namespace Turnero.Dto.Paciente
{
	public class PacienteConTurnoRequestDto: PacienteRequestDto
	{
		public TurnoRequestDto Turno { get; set; } = null!;
	}
}
