namespace Turnero.Dto.TurnoDto
{
    public class ModificarEstadoTurnoDto
    {
        public int IdTurno { get; set; }
        public int DniDelCancelador { get; set; }
        public string NuevoEstado { get; set; }
    }
}
