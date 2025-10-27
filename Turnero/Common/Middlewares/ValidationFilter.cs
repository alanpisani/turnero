using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation;
using Turnero.Dto;

namespace Turnero.Common.Middlewares;

public class ValidationFilter : IActionFilter
{
	public void OnActionExecuting(ActionExecutingContext context)
	{
		// context.ModelState contiene los errores de validación automáticos
		if (!context.ModelState.IsValid)
		{
			var errors = context.ModelState
				.Where(x => x.Value.Errors.Count > 0)
				.ToDictionary(
					kvp => kvp.Key,
					kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
				);

			var response = new ResponseDto<object>
			{
				Success = false,
				Errors = errors
			};

			context.Result = new JsonResult(response)
			{
				StatusCode = 200 // mantenemos 200 OK para controlados
			};
		}
	}

	public void OnActionExecuted(ActionExecutedContext context)
	{
		// no hacemos nada después de la acción
	}
}

