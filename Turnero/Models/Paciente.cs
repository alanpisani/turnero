using System.Text.Json.Serialization;

namespace Turnero.Models;

public partial class Paciente
{
    public int IdUsuario { get; set; }

    public string Telefono { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<CoberturaPaciente> CoberturaPacientes { get; set; } = new List<CoberturaPaciente>();
    [JsonIgnore]
    public virtual ICollection<Consultum> Consulta { get; set; } = new List<Consultum>();
    [JsonIgnore]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();

    public Paciente(int IdUsuario, string Telefono)
    {
        this.IdUsuario = IdUsuario;
        this.Telefono = Telefono;
    }
}
