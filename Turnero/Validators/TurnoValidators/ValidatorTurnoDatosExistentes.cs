using Turnero.Dto;
using Turnero.Repositories.Interfaces;

namespace Turnero.Validators.TurnoValidators
{
	public class ValidatorTurnoDatosExistentes(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _uow = unit;

		public async Task<ValidationResult> ValidarTurnoDatosExistentes(TurnoDto dto)
		{
			var result = new ValidationResult();

			if (!await PacienteExiste(dto))
			{
				result.Mensajes.Add("El paciente ingresado no se encuentra registrado en el sistema");
			}

			if(!await ProfesionalExiste(dto))
			{
				result.Mensajes.Add("El profesional ingresado no se encuentra registrado en el sistema");
			}

			if(!await EsEspecialidadValida(dto))
			{
				result.Mensajes.Add("La especialidad elegida no existe");
			}

			if(!await ProfesionalTieneEsaEspecialidad(dto))
			{
				result.Mensajes.Add("El profesional no posee la especialidad elegida");
			}

			result.EsValido = result.Mensajes.Count == 0;
			return result;
		}
		private async Task<bool> PacienteExiste(TurnoDto dto)
		{
			return await _uow.Pacientes.AnyPaciente(dto.IdPaciente);
		}

		private async Task<bool> ProfesionalExiste(TurnoDto dto)
		{
			return await _uow.Profesionales.AnyProfesional(dto.IdProfesional);
		}

		private async Task<bool> EsEspecialidadValida(TurnoDto dto)
		{
			return await _uow.Especialidades.AnyEspecialidad(dto.IdEspecialidad);
		}

		private async Task<bool> ProfesionalTieneEsaEspecialidad(TurnoDto dto)
		{
			return await _uow.Profesionales.AnyProfesionalWithThatSpeciality(
				idProfesional: dto.IdProfesional,
				idEspecialidad: dto.IdEspecialidad
				);
		}
	}
}
