namespace Turnero.Dto.Profesional
{
	public class ProfesionalResponseDto
	{
		public int IdUsuario { get; set; }
		public string NombreProfesional { get; set; } = string.Empty;
		public string ApellidoProfesional { get; set; } = string.Empty;
		public int Matricula {  get; set; }
		public bool? IsActive { get; set; } = true;

	}
}
