using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class Paciente
{
    public int IdUsuario { get; set; }

    public string? Telefono { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
