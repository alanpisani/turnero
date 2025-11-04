using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Turnero.Dto.Usuario
{
    public class UsuarioRequestDto
    {
        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public int Dni { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
        public string ContraseniaRepetida { get; set; } = string.Empty;
        public bool IsComplete { get; set; } = false;

        public UsuarioRequestDto() { }

        public UsuarioRequestDto(string Nombre, string Apellido, int Dni, string Email, string Contrasenia, string ContraseniaRepetida, bool isComplete)
        {

            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Dni = Dni;
            this.Email = Email;
            this.Contrasenia = Contrasenia;
            this.ContraseniaRepetida = ContraseniaRepetida;
            this.IsComplete = isComplete;
        }

    }
}
