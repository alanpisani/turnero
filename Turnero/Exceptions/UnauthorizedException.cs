namespace Turnero.Exceptions
{
	public class UnauthorizedException: Exception
	{
		public Dictionary<string, List<string>> Errors { get; }
		public UnauthorizedException(string mensaje):base(mensaje) {

			Errors = new Dictionary<string, List<string>>
				{
					{ "Errors", new List<string> { mensaje } }
				};
		}
	}
}
