namespace Turnero.Dto.Usuario
{
	public class UsuarioResponseDto
	{
		public int IdUsuario {  get; set; }
		public string Nombre { get; set; }

		public string Apellido { get; set; }

		public int Dni { get; set; }
		public string Email { get; set; }
		public string Rol {  get; set; } 

		public bool IsActive { get; set; } = true;
	}
}
