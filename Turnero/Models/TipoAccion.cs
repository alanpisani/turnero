using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class TipoAccion
{
    public int IdAccion { get; set; }

    public string Accion { get; set; } = null!;

    public virtual ICollection<HistorialTurno> HistorialTurnos { get; set; } = new List<HistorialTurno>();
}
