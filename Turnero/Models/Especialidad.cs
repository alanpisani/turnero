using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class Especialidad
{
    public int IdEspecialidad { get; set; }

    public string NombreEspecialidad { get; set; } = null!;

    public virtual ICollection<ProfesionalEspecialidad> ProfesionalEspecialidads { get; set; } = new List<ProfesionalEspecialidad>();
}
