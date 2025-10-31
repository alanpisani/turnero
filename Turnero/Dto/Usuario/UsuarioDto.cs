using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Turnero.Dto.Usuario
{
    public class UsuarioDto
    {
        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public int Dni { get; set; }
        public string Email { get; set; } = string.Empty;

        public string FechaNacimiento { get; set; } = string.Empty;

        public string Contrasenia { get; set; } = string.Empty;
        public string ContraseniaRepetida { get; set; } = string.Empty;
        public int IsComplete { get; set; } = 0;

        public UsuarioDto() { }

        public UsuarioDto(string Nombre, string Apellido, int Dni, string Email, string FechaNacimiento, string Contrasenia, string ContraseniaRepetida, int isComplete)
        {

            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Dni = Dni;
            this.Email = Email;
            this.FechaNacimiento = FechaNacimiento;
            this.Contrasenia = Contrasenia;
            this.ContraseniaRepetida = ContraseniaRepetida;
            IsComplete = isComplete;
        }

    }
}
