using Turnero.Exceptions;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Service
{
	public class CoberturaMedicaService(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;

		public async Task<List<CoberturaMedica>> MostrarTodasLasCoberturas()
		{
			
			try
			{
				var coberturas = await _unitOfWork.CoberturasMedicas.GetCoberturas();

				return coberturas;
			}
			catch
			{
				throw new Exception("Hubo un error al consultar las coberturas médicas. Inténtelo más tarde.");

			}
		}

		public async Task<CoberturaMedica> MostrarCoberturaPorId(int id)
		{
			try
			{
				var cobertura = await _unitOfWork.CoberturasMedicas.GetCoberturaById(id);

				if (cobertura == null) throw new NotFoundException("La cobertura no se encuentra registrada en el sistema");



				return cobertura;
			}
			catch
			{
				throw new Exception("hubo un error desconocido al intentar mostrar la cobertura.Inténtelo más tarde");
			}
		}
		
	}
}
