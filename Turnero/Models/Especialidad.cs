using System.Text.Json.Serialization;

namespace Turnero.Models;

public partial class Especialidad
{
    public int IdEspecialidad { get; set; }

    public string NombreEspecialidad { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ProfesionalEspecialidad> ProfesionalEspecialidads { get; set; } = new List<ProfesionalEspecialidad>();
}
