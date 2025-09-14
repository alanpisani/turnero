namespace Turnero.Validators
{
	public class ValidationResult
	{
		public bool EsValido { get; set; }
		public List<string> Mensajes { get; set; } = [];

		public ValidationResult() { 
			EsValido = false;
			Mensajes = [];
		}
	}
}
