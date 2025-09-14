using Microsoft.AspNetCore.Mvc.ModelBinding;
using Turnero.Service;

namespace Turnero.Common.Helpers
{
	public static class ResponseHelper
	{
		public static ServiceResponse<List<string>> FromModelState(ModelStateDictionary modelState)
		{
			var errors = modelState
				.SelectMany(ms => ms.Value!.Errors.Select(e =>
					$"{ms.Key}: {e.ErrorMessage}"))
				.ToList();

			return new ServiceResponse<List<string>>
			{
				Exito = false,
				Mensaje = "Errores de validación encontrados",
				Errores = errors,
			};
		}
	}
}
