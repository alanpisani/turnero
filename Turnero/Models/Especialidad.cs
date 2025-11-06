using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class Especialidad
{
    public int IdEspecialidad { get; set; }

    public string NombreEspecialidad { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<ProfesionalEspecialidad> ProfesionalEspecialidads { get; set; } = new List<ProfesionalEspecialidad>();

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
