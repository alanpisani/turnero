using System.Text.Json.Serialization;

namespace Turnero.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public int Dni { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public int IdRol { get; set; }
    [JsonIgnore]
    public virtual ICollection<HistorialTurno> HistorialTurnos { get; set; } = new List<HistorialTurno>();
    [JsonIgnore]
	public virtual ICollection<AuthToken> AuthTokens { get; set; }
	[JsonIgnore]
    public virtual Rol IdRolNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Paciente? Paciente { get; set; }
    [JsonIgnore]
    public virtual Profesional? Profesional { get; set; }

    public Usuario(string Nombre, string Apellido, string Email, string Contrasenia, int Dni, DateOnly FechaNacimiento, int IdRol)
    {
        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.Email = Email;
        this.Contrasenia = Contrasenia;
        this.Dni = Dni;
        this.FechaNacimiento = FechaNacimiento;
        this.IdRol = IdRol;
    }
}
