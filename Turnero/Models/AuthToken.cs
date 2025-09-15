using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Turnero.Models;

public partial class AuthToken
{
    public int IdAuthToken { get; set; }

    public int IdUsuario { get; set; }

    public string Token { get; set; } = null!;

    public bool? Activo { get; set; }

    public DateTime Expiracion { get; set; }

    [JsonIgnore]
    public virtual Usuario Usuario { get; set; } = null!;
}
