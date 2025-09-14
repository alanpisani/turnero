using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Turnero.Models;

public partial class Turno
{
    public int IdTurno { get; set; }

    public int IdEstadoTurno { get; set; }

    public int IdPaciente { get; set; }

    public int IdProfesional { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaTurno { get; set; }
    [JsonIgnore]
    public virtual ICollection<HistorialTurno> HistorialTurnos { get; set; } = new List<HistorialTurno>();
	[JsonIgnore]
	public virtual EstadoTurno IdEstadoTurnoNavigation { get; set; } = null!;
	[JsonIgnore]
	public virtual Paciente IdPacienteNavigation { get; set; } = null!;
	[JsonIgnore]
	public virtual Profesional IdProfesionalNavigation { get; set; } = null!;

    public Turno(int IdEstadoTurno, int IdPaciente, int IdProfesional, DateTime FechaTurno) {

        this.IdEstadoTurno = IdEstadoTurno;
        this.IdPaciente = IdPaciente;
        this.IdProfesional = IdProfesional;
        this.FechaTurno = FechaTurno;

    }
}
