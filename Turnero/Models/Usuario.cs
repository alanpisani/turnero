using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Turnero.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellido { get; set; }

    public string? Email { get; set; }

    public string? Contrasenia { get; set; }

    public int Dni { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public int IdRol { get; set; }

    public bool IsComplete { get; set; }
    [JsonIgnore]
    public virtual ICollection<AuthToken> AuthTokens { get; set; } = new List<AuthToken>();
	[JsonIgnore]
	public virtual ICollection<CoberturaPaciente> CoberturaPacientes { get; set; } = new List<CoberturaPaciente>();
	[JsonIgnore]
	public virtual ICollection<HistorialTurno> HistorialTurnos { get; set; } = new List<HistorialTurno>();
	[JsonIgnore]
	public virtual Rol IdRolNavigation { get; set; } = null!;
	[JsonIgnore]
	public virtual Paciente? Paciente { get; set; }
	[JsonIgnore]
	public virtual Profesional? Profesional { get; set; }
	[JsonIgnore]
	public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
