namespace Turnero.Service
{
	public class ServiceResponse<T>
	{
		public bool Exito {  get; set; }
		public string Mensaje { get; set; } = string.Empty;
		public T? Cuerpo { get; set; }

		public List<string>? Errores { get; set; } = [];

		public ServiceResponse()
		{
			Exito = false;
			Mensaje = "Error al ingresar datos";
		}

	}
}
