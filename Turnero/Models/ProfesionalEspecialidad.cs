using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class ProfesionalEspecialidad
{
    public int IdProfesionalEspecialidad { get; set; }

    public int IdProfesional { get; set; }

    public int IdEspecialidad { get; set; }

    public virtual Especialidad IdEspecialidadNavigation { get; set; } = null!;

    public virtual Profesional IdProfesionalNavigation { get; set; } = null!;
}
