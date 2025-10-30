namespace Turnero.Dto.TurnoDto
{
	public class TurnoRequest
	{
		public int IdEspecialidad { get; set; }
		public int IdProfesional { get; set; }
		public string Dia { get; set; } = string.Empty;
		public string Hora { get; set; } = string.Empty;
	}
}
