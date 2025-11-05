using Turnero.Dto.TurnoDto;

namespace Turnero.Dto.Paciente
{
	public class PacienteYTurnoResponseDto
	{
		public PacienteResponseDto Paciente { get; set; } = null!;
		public TurnoResponseDto Turno { get; set; } = null!;
	}
}
