using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class Turno
{
    public int IdTurno { get; set; }

    public string EstadoTurno { get; set; } = null!;

    public int IdPaciente { get; set; }

    public int IdProfesional { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaTurno { get; set; }

    public int IdEspecialidad { get; set; }

    public virtual ICollection<HistorialTurno> HistorialTurnos { get; set; } = new List<HistorialTurno>();

    public virtual Especialidad IdEspecialidadNavigation { get; set; } = null!;

    public virtual Usuario IdPacienteNavigation { get; set; } = null!;

    public virtual Profesional IdProfesionalNavigation { get; set; } = null!;
}
