using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class CoberturaMedica
{
    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string Telefono { get; set; } = null!;

    public string TipoCobertura { get; set; } = null!;

    public int IdCoberturaMedica { get; set; }

    public virtual ICollection<CoberturaPaciente> CoberturaPacientes { get; set; } = new List<CoberturaPaciente>();
}
