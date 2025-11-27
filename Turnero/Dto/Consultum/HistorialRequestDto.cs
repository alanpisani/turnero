namespace Turnero.Dto.Consultum
{
	public class HistorialRequestDto
	{
		public int IdTurno { get; set; }
		public string Diagnostico {  get; set; } = string.Empty;
		public string Tratamiento {  get; set; } = string.Empty;
		public string Observaciones { get; set; } = "Sin observaciones";

	}
}
