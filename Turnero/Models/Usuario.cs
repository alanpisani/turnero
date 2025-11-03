using System;
using System.Collections.Generic;

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

    public string Rol { get; set; } = null!;

    public bool IsComplete { get; set; }

    public virtual ICollection<AuthToken> AuthTokens { get; set; } = new List<AuthToken>();

    public virtual ICollection<CoberturaPaciente> CoberturaPacientes { get; set; } = new List<CoberturaPaciente>();

    public virtual ICollection<HistorialTurno> HistorialTurnos { get; set; } = new List<HistorialTurno>();

    public virtual Paciente? Paciente { get; set; }

    public virtual Profesional? Profesional { get; set; }

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
