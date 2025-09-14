using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Validators.PacienteValidators
{
	public class ValidatorCobertura(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;
		public async Task<ValidationResult> ValidarCoberturas(PacienteDto dto)
		{
			var result = new ValidationResult();

			if (dto.CoberturasMedicas != null)
			{
				if (HayCoberturasRepetidasEnRegistro(dto))
				{
					result.Mensajes.Add("No se pueden agregar dos veces la misma cobertura médica");
				}
				if(await HayMasDeUnaObraSocialEnRegistro(dto))
				{
					result.Mensajes.Add("No se pueden agregar dos obras sociales");
				}

				result.EsValido = result.Mensajes.Count == 0;
				return result;


			}
			result.EsValido = true;
			return result;
		}

		private bool HayCoberturasRepetidasEnRegistro(PacienteDto dto)
		{			
			var idsDuplicados = dto.CoberturasMedicas! //lista de coberturas duplicadas que registra el paciente
				.GroupBy(c => c.IdCobertura) //Agrupa en "bolsas" cada cobertura que compartan id
				.Where(g => g.Count() > 1) //Filtra las que haya mas de 1 elemento en cada bolsa
				.Select(g => g.Key) //toma solo el id, o mejor dicho, key de cada bolsa, o los elementos, creo. No se
				.ToList();

			return idsDuplicados.Count > 0; //Si hay bolsas, basicamente. Esto indicaria que hubo coberturas repetidas al registrar paciente
		}

		private async Task<bool> HayMasDeUnaObraSocialEnRegistro(PacienteDto dto)
		{
			var idsObrasSociales = await _unitOfWork.Pacientes.ToListAsyncIdsObrasSociales();

			var cantObrasSocialesEnDto = 0;

			foreach (var coberturaDto in dto.CoberturasMedicas!)
			{
				if (idsObrasSociales.Contains(coberturaDto.IdCobertura))
				{
					cantObrasSocialesEnDto++;
				}

				if (cantObrasSocialesEnDto > 1)
				{
					return true;
				}
			}
			return false;
		}
	}
}
