using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class FluentValidationFilter : IAsyncActionFilter
{
	private readonly IServiceProvider _serviceProvider;

	public FluentValidationFilter(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		foreach (var argument in context.ActionArguments.Values)
		{
			if (argument == null) continue;

			var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
			var validator = _serviceProvider.GetService(validatorType) as IValidator;

			if (validator != null)
			{
				var result = await validator.ValidateAsync(new ValidationContext<object>(argument));
				if (!result.IsValid)
				{
					// Agrupar errores por propiedad
					var errors = result.Errors
						.GroupBy(e => e.PropertyName)
						.ToDictionary(
							g => g.Key,
							g => g.Select(e => e.ErrorMessage).ToList()
						);

					context.Result = new BadRequestObjectResult(new { errors = errors });
					return;
				}
			}
		}

		await next();
	}
}
 