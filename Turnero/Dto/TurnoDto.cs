using System.ComponentModel.DataAnnotations;

namespace Turnero.Dto
{
	public class TurnoDto
	{
		[Required(ErrorMessage = "Paciente es requerido para solicitar turno")]
		public int IdPaciente { get; set; }
		[Required(ErrorMessage = "Especialidad requerida para solicitar turno")]
		public int IdEspecialidad { get; set; }
		[Required(ErrorMessage = "Profesional requerido para solicitar turno")]
		public int IdProfesional { get; set; }
		public string Dia {  get; set; } = string.Empty;
		public string Hora { get; set; } = string.Empty;


	}
}
