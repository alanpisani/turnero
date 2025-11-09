using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class Paciente
{
    public int IdUsuario { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
