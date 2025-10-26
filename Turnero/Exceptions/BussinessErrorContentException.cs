using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Turnero.Exceptions
{
    public class BussinessErrorContentException : Exception
    {
        public Dictionary<string, List<string>> Errors { get; }

        public BussinessErrorContentException(Dictionary<string, List<string>> errores)
            : base()
        {
            Errors = errores;
        }

        public BussinessErrorContentException(Dictionary<string, List<string>> errores, Exception inner)
            : base("Error en la logica de negocio", inner)
        {
            Errors = errores;
        }
    }
}
