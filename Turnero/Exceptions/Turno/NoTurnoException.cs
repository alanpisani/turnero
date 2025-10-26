namespace Turnero.Exceptions.Turno
{
	public class NoTurnoException: Exception
	{
		public Dictionary<string, List<string>> Errors { get; }

		public NoTurnoException(string mensaje)
		: base(mensaje)
			{
				Errors = new Dictionary<string, List<string>>
					{
						{ "Errors", new List<string> { mensaje } }
					};
			}
	}
}
