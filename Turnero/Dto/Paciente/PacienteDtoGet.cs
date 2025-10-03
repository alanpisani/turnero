namespace Turnero.Dto.Paciente
{
	public class PacienteDtoGet
	{
		public int Id { get; set; }
		public string Nombre { get; set; } = null!;
		public string Apellido { get; set; } = null!;
		public int Dni {  get; set; }
		public string Email { get; set; } = null!;
		public string FechaNacimiento { get; set; } = null!;
		public string Telefono { get; set; } = null!;
	}
}
