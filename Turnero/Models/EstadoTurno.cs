using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class EstadoTurno
{
    public int IdEstadoTurno { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
