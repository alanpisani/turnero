using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class HistorialTurno
{
    public int IdHistorialTurno { get; set; }

    public int IdTurno { get; set; }

    public int IdTipoAccion { get; set; }

    public int IdUsuario { get; set; }

    public DateTime FechaCambio { get; set; }

    public string? Motivo { get; set; }

    public virtual TipoAccion IdTipoAccionNavigation { get; set; } = null!;

    public virtual Turno IdTurnoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
