using System.ComponentModel.DataAnnotations;

namespace Turnero.Dto.Profesional
{
    public class ProfesionalRequestDto : UsuarioDto
    {
        public int Matricula { get; set; }
        public List<int> Especialidades { get; set; } = [];
        public List<HorarioLaboralDto> HorariosLaborales { get; set; } = [];

        public ProfesionalRequestDto() { }
        public ProfesionalRequestDto(string Nombre, string Apellido, string Email, int Dni, string FechaNacimiento,
            string Contrasenia, string ContraseniaRepetida, int Matricula, List<HorarioLaboralDto> HorariosLaborales, List<int> Especialidades
        )
        {
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
