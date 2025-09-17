using Turnero.Common.Helpers;
using Turnero.Dto;
using Turnero.Repositories.Interfaces;
using Turnero.Validators.UsuarioValidators;

namespace Turnero.Domain.PacienteDomain
{
	public class CreatePacienteDomain(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;
		public async Task<DomainValidationResult> Validar(PacienteDto dto)
		{
			var result = new DomainValidationResult();

			if (!NoHayCoberturasRepetidasEnRegistro(dto.CoberturasMedicas))
			{
				result.AddError("CoberturasMedicas", "No se pueden agregar dos veces la misma cobertura médica");
			}

			if(!await ObraSocialSinRepetirEnRegistro(dto.CoberturasMedicas))
			{
				result.AddError("CoberturasMedicas", "No se pueden agregar dos obras sociales");
			}

			result.EsValido = result.Errores.Count == 0;
			return result;
		}
		private bool NoHayCoberturasRepetidasEnRegistro(List<CoberturaPacienteDto>? lista)
		{
			if (lista == null) return true;

			var idsDuplicados = lista //lista de coberturas duplicadas que registra el paciente
				.GroupBy(c => c.IdCobertura) //Agrupa en "bolsas" cada cobertura que compartan id
				.Where(g => g.Count() > 1) //Filtra las que haya mas de 1 elemento en cada bolsa
				.Select(g => g.Key) //toma solo el id, o mejor dicho, key de cada bolsa, o los elementos, creo. No se
				.ToList();

			return idsDuplicados.Count == 0; //Si no hay bolsas, basicamente. Esto indicaria que no hubo coberturas repetidas al registrar paciente
		}

		private async Task<bool> ObraSocialSinRepetirEnRegistro(List<CoberturaPacienteDto>? lista)
		{
			if (lista == null) return true;

			var idsObrasSociales = await _unitOfWork.Pacientes.ToListAsyncIdsObrasSociales();

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
