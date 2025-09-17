namespace Turnero.Common.Helpers
{
	public class DomainValidationResult
	{
		public bool EsValido { get; set; }
		public Dictionary<string, List<string>> Errores { get; set; } = [];

		public void AddError(string propiedad, string error)
		{
			if (!Errores.ContainsKey(propiedad))
			{
				Errores[propiedad] = [];
			}
			Errores[propiedad].Add(error);
		}
	}
}
