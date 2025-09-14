using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class CoberturaPaciente
{
    public int IdCoberturaPaciente { get; set; }

    public string NumeroAfiliado { get; set; } = null!;

    public int IdPaciente { get; set; }

    public int IdCoberturaMedica { get; set; }

    public virtual CoberturaMedica IdCoberturaMedicaNavigation { get; set; } = null!;

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;
}
