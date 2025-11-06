namespace Turnero.Dto.Especialidad
{
	public class EspecialidadResponseDto
	{
		public int IdEspecialidad {  get; set; }
		public string NombreEspecialidad { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
