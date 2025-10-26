using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class Turno
{
    public int IdTurno { get; set; }

    public int IdEstadoTurno { get; set; }

    public int IdPaciente { get; set; }

    public int IdProfesional { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaTurno { get; set; }

    public virtual ICollection<HistorialTurno> HistorialTurnos { get; set; } = new List<HistorialTurno>();

    public virtual EstadoTurno IdEstadoTurnoNavigation { get; set; } = null!;

    public virtual Usuario IdPacienteNavigation { get; set; } = null!;

    public virtual Profesional IdProfesionalNavigation { get; set; } = null!;
}
