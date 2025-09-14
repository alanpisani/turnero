using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class Consultum
{
    public int IdConsulta { get; set; }

    public int IdPaciente { get; set; }

    public int IdProfesional { get; set; }

    public DateOnly FechaConsulta { get; set; }

    public string Diagnostico { get; set; } = null!;

    public string Tratamiento { get; set; } = null!;

    public string? Observaciones { get; set; }

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;

    public virtual Profesional IdProfesionalNavigation { get; set; } = null!;
}
