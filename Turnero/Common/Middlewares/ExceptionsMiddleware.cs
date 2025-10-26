using System.Net;
using Turnero.Exceptions;
using Turnero.Exceptions.Turno;

namespace Turnero.Common.Middlewares
{
    public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (UnauthorizedAccessException ex)
			{
				await HandleExceptionAsync(context, HttpStatusCode.Unauthorized, new
				{
					Errors = ex.Message
				});
			}
			catch (NotFoundException ex)
			{
				await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex.Errors);
			}
			catch (BussinessErrorContentException ex)
			{
				
				await HandleExceptionAsync(context, HttpStatusCode.Conflict, new
				{
					ex.Errors
				});
			}
			catch (NoTurnoException ex)
			{
				await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex.Errors);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, new
				{
					Errors= $"Error interno del servidor: {ex.Message}"
				});
			}
		}

		private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, object contenido)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)statusCode;

			
			await context.Response.WriteAsJsonAsync(contenido);
		}
	}
}
