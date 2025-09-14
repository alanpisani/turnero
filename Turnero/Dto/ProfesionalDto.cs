using System.ComponentModel.DataAnnotations;

namespace Turnero.Dto
{
	public class ProfesionalDto: UsuarioDto
	{
		[Required(ErrorMessage = "La matrícula es requerida")]

		public int Matricula { get; set; } = 0;
		[Required(ErrorMessage = "Especialidad requerida")]
		[MinLength(1, ErrorMessage = "Debe asignarle al menos una especialidad al profesional")]
		public List<int> Especialidades { get; set; } = [];
		[Required(ErrorMessage = "Horarios requeridos")]
		[MinLength(1, ErrorMessage = "Debe haber al menos un horario laboral")]
		public List<HorarioLaboralDto> HorariosLaborales { get; set; } = [];

		public ProfesionalDto() {}
		public ProfesionalDto(string Nombre, string Apellido, string Email, int Dni, string FechaNacimiento, 
			string Contrasenia, string ContraseniaRepetida, int Matricula, List<HorarioLaboralDto> HorariosLaborales, List<int> Especialidades
		) {
			this.Nombre = Nombre;
			this.Apellido = Apellido; 
			this.Email = Email;
			this.Dni = Dni;
			this.FechaNacimiento = FechaNacimiento;
			this.Matricula = Matricula;
			this.Contrasenia = Contrasenia;
			this.ContraseniaRepetida = ContraseniaRepetida;
			this.HorariosLaborales = HorariosLaborales;
			this.Especialidades = Especialidades;
		
		}
	}
}
