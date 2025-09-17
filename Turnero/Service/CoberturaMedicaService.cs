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

		public async Task<ServiceResponse<CoberturaMedica>> MostrarCoberturaPorId(int id)
		{
			try
			{
				var cobertura = await _unitOfWork.CoberturasMedicas.GetCoberturaById(id);

				if (cobertura == null)
				{
					return new ServiceResponse<CoberturaMedica>
					{
						Mensaje = "La cobertura no se encuentra registrada en el sistema"
					};
				}

				return new ServiceResponse<CoberturaMedica>
				{
					Exito = true,
					Mensaje = "Cobertura traiga con éxitazo",
					Cuerpo = cobertura,
				};
			}
			catch
			{
				return new ServiceResponse<CoberturaMedica>
				{
					Mensaje = "hubo un error desconocido al intentar mostrar la cobertura.Inténtelo más tarde"
				};
			}
		}
		
	}
}
