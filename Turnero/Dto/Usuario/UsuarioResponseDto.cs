namespace Turnero.Dto.Usuario
{
	public class UsuarioResponseDto
	{
		public string Nombre { get; set; }

		public string Apellido { get; set; }

		public int Dni { get; set; }
		public string Email { get; set; }
		public string Rol {  get; set; } 

		public bool IsComplete { get; set; } = false;
	}
}
