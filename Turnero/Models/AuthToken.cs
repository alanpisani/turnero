using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class AuthToken
{
    public int IdAuthToken { get; set; }

    public int IdUsuario { get; set; }

    public string? Token { get; set; }

    public bool? Activo { get; set; }

    public DateTime Expiracion { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
