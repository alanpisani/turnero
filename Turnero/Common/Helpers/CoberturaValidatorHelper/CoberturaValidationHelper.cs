using Turnero.Repositories.Interfaces;

namespace Turnero.Common.Helpers.CoberturaValidatorHelper
{
	public static class CoberturaValidationHelper
	{
		public static async Task<bool> ExisteCobertura(string nombre, IUnitOfWork unitOfWork)
		{
			return await unitOfWork.CoberturasMedicas.AnyCobertura(nombre);

		}
	}
}
