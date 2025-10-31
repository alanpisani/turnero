using System.ComponentModel.DataAnnotations;
using Turnero.Dto.Usuario;

namespace Turnero.Dto.Paciente
{
    public class PacienteRequestDto : UsuarioDto
    {
        public string Telefono { get; set; } = string.Empty;
        public List<CoberturaPacienteDto>? CoberturasMedicas { get; set; }
        public PacienteRequestDto() { }

        public PacienteRequestDto(string Nombre, string Apellido, int Dni, string Email, string FechaNacimiento, string Contrasenia, string ContraseniaRepetida, int IsComplete)
            : base(Nombre, Apellido, Dni, Email, FechaNacimiento, Contrasenia, ContraseniaRepetida, IsComplete) { }

        public PacienteRequestDto(
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
        ) : base(Nombre, Apellido, Dni, Email, FechaNacimiento, Contrasenia, ContraseniaRepetida, IsComplete)
        {
            this.Telefono = Telefono;
            this.CoberturasMedicas = CoberturasMedicas;
        }
    }
}