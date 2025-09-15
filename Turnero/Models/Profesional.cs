using System.Text.Json.Serialization;

namespace Turnero.Models;

public partial class Profesional(int IdUsuario, int Matricula)
{
    public int IdUsuario { get; set; } = IdUsuario;

    public int Matricula { get; set; } = Matricula;

    [JsonIgnore]
    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();
    [JsonIgnore]
    public virtual ICollection<HorarioLaboral> HorarioLaborals { get; set; } = new List<HorarioLaboral>();
    [JsonIgnore]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<ProfesionalEspecialidad> ProfesionalEspecialidads { get; set; } = new List<ProfesionalEspecialidad>();
    [JsonIgnore]
    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();

}
