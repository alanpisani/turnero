using Turnero.Models;
using Turnero.Repositories.Interfaces;
using Turnero.Validators.CoberturaMedicaValidators;

namespace Turnero.Service
{
	public class CoberturaMedicaService(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;

		public async Task<ServiceResponse<List<CoberturaMedica>>> MostrarTodasLasCoberturas()
		{
			
			try
			{
				var coberturas = await _unitOfWork.CoberturasMedicas.GetCoberturas();

				return new ServiceResponse<List<CoberturaMedica>>
				{
					Exito = true,
					Mensaje = "Coberturas consultadas con éxito",
					Cuerpo = coberturas,
				};
			}
			catch
			{
				return new ServiceResponse<List<CoberturaMedica>>
				{
					Mensaje = "Hubo un error al consultar las coberturas médicas. Inténtelo más tarde."
				};
			}
		}
		
	}
}
