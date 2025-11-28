 namespace Turnero.Dto.TurnoDto
{
	public class TurnsOfTheDayDto
	{
		public int IdTurno { get; set; }
		public int IdPaciente { get; set; }
		public string NombrePaciente {  set; get; } = string.Empty;
		public string Especialidad { set; get; } = string.Empty;
		public string Hora { set; get; } = string.Empty;
		public string Estado {  set; get; } = string.Empty;

	}
}
