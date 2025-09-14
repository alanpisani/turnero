namespace Turnero.Common.Helpers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;

	public class ValidateModelStateAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				context.Result = new BadRequestObjectResult(
					ResponseHelper.FromModelState(context.ModelState)
				);
			}
		}
	}

}
