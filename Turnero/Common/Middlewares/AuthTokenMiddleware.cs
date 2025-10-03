using Turnero.Repositories.Interfaces;

namespace Turnero.Common.Middlewares
{
	public class AuthTokenMiddleware(RequestDelegate next)
	{
		private readonly RequestDelegate _next = next;

		public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
		{
			// Busca si hay un token en authorization, allá afuera
			var token = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

			if (!string.IsNullOrEmpty(token)) //Si no está vacio el token, es decir, si hay uno
			{
				var authToken = await unitOfWork.AuthTokens //Busco en la bd si hay un token que coincida y que esté activo
					.FirstOrDefaultTokenActivo(token);

				if (authToken == null || authToken.Expiracion < DateTime.UtcNow) //En caso de no encontrarlo...
				{
					context.Response.StatusCode = StatusCodes.Status401Unauthorized; //Desautorizado. Venció el token o no existe
					await context.Response.WriteAsync("Token inválido o expirado");
					return;
				}

				//context.Items["UsuarioId"] = authToken.IdUsuario;
			}

			// next. A seguir con los controllers, pa
			await _next(context);
		}
	}
}
