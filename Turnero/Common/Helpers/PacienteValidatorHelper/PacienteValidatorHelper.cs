using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Common.Helpers.PacienteValidatorHelper
{
	public static class PacienteValidatorHelper
	{
		public static bool NoHayCoberturasRepetidasEnRegistro(List<CoberturaPacienteDto>? lista)
		{
			if (lista == null) return true;

			var idsDuplicados = lista //lista de coberturas duplicadas que registra el paciente
				.GroupBy(c => c.IdCobertura) //Agrupa en "bolsas" cada cobertura que compartan id
				.Where(g => g.Count() > 1) //Filtra las que haya mas de 1 elemento en cada bolsa
				.Select(g => g.Key) //toma solo el id, o mejor dicho, key de cada bolsa, o los elementos, creo. No se
				.ToList();

			return idsDuplicados.Count == 0; //Si no hay bolsas, basicamente. Esto indicaria que no hubo coberturas repetidas al registrar paciente
		}

		public static async Task<bool> ObraSocialSinRepetirEnRegistro(List<CoberturaPacienteDto>? lista, IUnitOfWork unitOfWork)
		{
			if (lista == null) return true;

			var idsObrasSociales = await unitOfWork.Pacientes.ToListAsyncIdsObrasSociales();

			var cantObrasSocialesEnDto = 0;

			foreach (var coberturaDto in lista)
			{
				if (idsObrasSociales.Contains(coberturaDto.IdCobertura))
				{
					cantObrasSocialesEnDto++;
				}

				if (cantObrasSocialesEnDto > 1)
				{
					return false;
				}
			}
			return true;
		}
	}
}