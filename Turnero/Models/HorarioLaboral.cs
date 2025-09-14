using System;
using System.Collections.Generic;

namespace Turnero.Models;

public partial class HorarioLaboral
{
    public int IdHorarioLaboral { get; set; }

    public int IdProfesional { get; set; }

    public sbyte DiaSemana { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public sbyte DuracionTurno { get; set; }

    public bool? Activo { get; set; }

    public virtual Profesional IdProfesionalNavigation { get; set; } = null!;
}
