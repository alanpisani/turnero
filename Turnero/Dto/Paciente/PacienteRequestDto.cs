using System.ComponentModel.DataAnnotations;
using Turnero.Dto.Usuario;

namespace Turnero.Dto.Paciente
{
    public class PacienteRequestDto : UsuarioRequestDto
    {
        public string Telefono { get; set; } = string.Empty;
        public PacienteRequestDto() { }

        public PacienteRequestDto(string Nombre, string Apellido, int Dni, string Email, string Contrasenia, string ContraseniaRepetida, bool IsComplete)
            : base(Nombre, Apellido, Dni, Email, Contrasenia, ContraseniaRepetida, IsComplete) { }

        public PacienteRequestDto(
            string Nombre,
            string Apellido,
            int Dni,
            string Email,
            string Contrasenia,
            string ContraseniaRepetida,
            bool IsComplete,
            string Telefono
        ) : base(Nombre, Apellido, Dni, Email, Contrasenia, ContraseniaRepetida, IsComplete)
        {
            this.Telefono = Telefono;
        }
    }
}