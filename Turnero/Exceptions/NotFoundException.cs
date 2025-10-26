namespace Turnero.Exceptions
{
    public class NotFoundException : Exception
    {
        public Dictionary<string, List<string>> Errors { get; }

        public NotFoundException(string mensaje)
            : base(mensaje)
        {
            Errors = new Dictionary<string, List<string>>
                {
                    { "Errors", new List<string> { mensaje } }
                };
        }

    }
}
