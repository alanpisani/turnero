using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Validators.ProfesionalValidators
{
    public class ProfesionalEspecialidadesValidator(IUnitOfWork unit)
    {
        private readonly IUnitOfWork _unitOfWork = unit;

        public async Task<ValidationResult> ValidarEspecialidades(ProfesionalDto dto)
        {
            var result = new ValidationResult();

            if (HayEspecialidadesRepetidas(dto))
            {
                result.Mensajes.Add("Se ingresaron dos o más veces la misma especialidad para el profesional");
            }
            if (!await EspecialidadExiste(dto)) {
                result.Mensajes.Add("La especialidad ingresada no existe");
            }

            result.EsValido = result.Mensajes.Count == 0;
            return result;
        }

        private bool HayEspecialidadesRepetidas(ProfesionalDto dto)
        {
            var especialidadesDuplicadas = dto.Especialidades
                .GroupBy(g => g)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            return especialidadesDuplicadas.Count > 0;
        }

        private async Task<bool> EspecialidadExiste(ProfesionalDto dto) {

			var todasLasEspecialidades = await _unitOfWork.Especialidades.ToListAsyncEspecialidades();

			foreach (var especialidadDto in dto.Especialidades)
			{
				var existeEspecialidad = todasLasEspecialidades.Any(e => e.IdEspecialidad == especialidadDto);
				if (!existeEspecialidad)
				{
                    return false;
				}
			}

            return true;
		}
    }
}
