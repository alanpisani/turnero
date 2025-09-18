using System.ComponentModel.DataAnnotations;

namespace Turnero.Dto
{
	public class HorarioLaboralDto
	{
		[Required]
		public int DiaLaboral { get; set; } = 0;
		[Required]
		public int DuracionTurno { get; set; } = 0;
		[Required]
		public string HoraInicio { get; set; } = string.Empty;
		[Required]
		public string HoraFin { get; set; } = string.Empty;

		public HorarioLaboralDto() { }
	}
}
