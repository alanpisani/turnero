using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class HistorialClinico
{
    public int IdHistorial { get; set; }

    public int IdTurno { get; set; }

    public string Diagnostico { get; set; } = null!;

    public string Tratamiento { get; set; } = null!;

    public string? Observaciones { get; set; }

    public virtual Turno IdTurnoNavigation { get; set; } = null!;
}
