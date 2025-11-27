namespace Turnero.Dto.Consultum
{
	public class HistorialResponseDto
	{
		public int IdHistorial { get; set; }
		public string NombrePaciente { get; set; } = string.Empty;
		public string FechaConsulta { get; set; } = string.Empty;

		public string Diagnostico {  get; set; } = string.Empty;
		public string Tratamiento { get; set; } = string.Empty;
		public string Observaciones {  get; set; } = string.Empty;

	}
}
