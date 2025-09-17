using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Turnero.Models;

public partial class CoberturaMedica
{
    [Required(ErrorMessage = "El nombre de la cobertura es obligatorio")]
    public string Nombre { get; set; } = null!;
    public string? Direccion { get; set; } = "Sin información";
    [Required(ErrorMessage = "El teléfono es obligatorio")]
    public string Telefono { get; set; } = null!;
    [Required(ErrorMessage = "Debe elegir un tipo de cobertura")]
    public string TipoCobertura { get; set; } = null!;
    public int IdCoberturaMedica { get; set; } = 0;
    [JsonIgnore]
    public virtual CoberturaPaciente? CoberturaPaciente { get; set; }
}
