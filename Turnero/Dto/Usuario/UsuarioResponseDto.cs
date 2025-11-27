namespace Turnero.Dto.Usuario
{
	public class UsuarioResponseDto
	{
		public int IdUsuario {  get; set; }
		public string Nombre { get; set; } = string.Empty;

		public string Apellido { get; set; } = string.Empty;

		public int Dni { get; set; }
		public string Email { get; set; } = string.Empty;
		public string Rol { get; set; } = "Paciente";

		public bool? IsActive { get; set; } = true;
	}
}
