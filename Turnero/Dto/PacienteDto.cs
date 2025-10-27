using System.ComponentModel.DataAnnotations;

namespace Turnero.Dto
{
	public class PacienteDto: UsuarioDto
	{
		public string Telefono { get; set; } = string.Empty;
		public List<CoberturaPacienteDto>? CoberturasMedicas { get; set; }
		public PacienteDto() { }

		public PacienteDto(string Nombre, string Apellido, int Dni, string Email, string FechaNacimiento, string Contrasenia, string ContraseniaRepetida, int IsComplete)
			: base(Nombre, Apellido, Dni, Email, FechaNacimiento, Contrasenia, ContraseniaRepetida, IsComplete) { }

		public PacienteDto(
			string Nombre,
			string Apellido, 
			int Dni, 
			string Email, 
			string FechaNacimiento, 
			string Contrasenia, 
			string ContraseniaRepetida,
			int IsComplete,
			string Telefono,
			List<CoberturaPacienteDto> CoberturasMedicas
		): base(Nombre, Apellido, Dni, Email, FechaNacimiento, Contrasenia, ContraseniaRepetida, IsComplete) { 
			this.Telefono = Telefono;
			this.CoberturasMedicas = CoberturasMedicas;
		}
	}
}