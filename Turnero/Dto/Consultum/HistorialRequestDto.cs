namespace Turnero.Dto.Consultum
{
	public class HistorialRequestDto
	{
		public int IdTurno { get; set; }
		public string FechaConsulta { get; set; } = string.Empty;
		public string Diagnostico {  get; set; } = string.Empty;
		public string Tratamiento {  get; set; } = string.Empty;
		public string Observaciones { get; set; } = string.Empty;

	}
}
