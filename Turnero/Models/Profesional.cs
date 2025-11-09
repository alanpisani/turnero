using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class Profesional
{
    public int IdUsuario { get; set; }

    public int Matricula { get; set; }

    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();

    public virtual ICollection<HorarioLaboral> HorarioLaborals { get; set; } = new List<HorarioLaboral>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<ProfesionalEspecialidad> ProfesionalEspecialidads { get; set; } = new List<ProfesionalEspecialidad>();

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
