using System.ComponentModel.DataAnnotations;

namespace Turnero.Dto
{
	public class PacienteDto: UsuarioDto
	{
		public string Telefono { get; set; } = string.Empty;
		public List<CoberturaPacienteDto>? CoberturasMedicas { get; set; }
		public PacienteDto() { }

		public PacienteDto(string Nombre, string Apellido, int Dni, string Email, string FechaNacimiento, string Contrasenia, string ContraseniaRepetida)
			: base(Nombre, Apellido, Dni, Email, FechaNacimiento, Contrasenia, ContraseniaRepetida) { }

		public PacienteDto(
			string Nombre,
			string Apellido, 
			int Dni, 
			string Email, 
			string FechaNacimiento, 
			string Contrasenia, 
			string ContraseniaRepetida, 
			string Telefono,
			List<CoberturaPacienteDto> CoberturasMedicas
		): base(Nombre, Apellido, Dni, Email, FechaNacimiento, Contrasenia, ContraseniaRepetida) { 
			this.Telefono = Telefono;
			this.CoberturasMedicas = CoberturasMedicas;
		}
	}
}