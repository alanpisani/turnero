using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Turnero.Dto
{
	public class UsuarioDto
	{
		[Required(ErrorMessage = "El nombre es requerido")]
		[StringLength(20, ErrorMessage = "El nombre es demasiado extenso")]
		public string Nombre { get; set; } = string.Empty;

		[Required(ErrorMessage = "El apellido es requerido")]
		[StringLength(20, ErrorMessage = "El apellido es muy extenso")]
		public string Apellido { get; set; } = string.Empty ;

		[Required(ErrorMessage = "El DNI es requerido")]
		[Range(10000000, 99999999, ErrorMessage = ("Debe escribir un DNI válido"))]
		public int Dni {  get; set; }

		[Required(ErrorMessage = "El correo electrónico es requerido")]
		[RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org)$", ErrorMessage = "El correo debe ser válido")]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "El mail no puede ser tan extenso")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "La fecha de nacimiento es requerida")]
		public string FechaNacimiento { get; set; } = string.Empty;

		[Required(ErrorMessage = "La contraseña es requerida")]
		public string Contrasenia {  get; set; } = string.Empty;

		[Required(ErrorMessage = "Debe completar el campo requerido")]
		[Compare("Contrasenia", ErrorMessage = "Las contraseñas no coinciden")]
		public string ContraseniaRepetida {  get; set; } = string.Empty;

		//[JsonIgnore]
		//public DateOnly GetFechaNacimiento => DateOnly.Parse(FechaNacimiento);

		public UsuarioDto() { }

		public UsuarioDto(string Nombre, string Apellido, int Dni, string Email, string FechaNacimiento, string Contrasenia, string ContraseniaRepetida) {
		
			this.Nombre = Nombre;
			this.Apellido = Apellido;
			this.Dni = Dni;
			this.Email = Email;
			this.FechaNacimiento = FechaNacimiento;
			this.Contrasenia = Contrasenia;
			this.ContraseniaRepetida = ContraseniaRepetida;
		}

	}
}
