using System.ComponentModel.DataAnnotations;

namespace Turnero.Dto
{
	public class TurnoDto
	{
		public int IdPaciente { get; set; }
		public int IdEspecialidad { get; set; }
		public int IdProfesional { get; set; }
		public string Dia {  get; set; } = string.Empty;
		public string Hora { get; set; } = string.Empty;
	}
}
