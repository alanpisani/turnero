using Turnero.Dto;
using Turnero.Repositories.Interfaces;
using Turnero.Validators.UsuarioValidators;

namespace Turnero.Validators.ProfesionalValidators
{
	public class ProfesionalValidator(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;

		public async Task<ValidationResult> ValidarProfesional(ProfesionalDto dto)
		{
			var validator = new ValidationResult();
			var validacionUsuario = await new ValidatorUsuario(_unitOfWork).ValidarUsuario(dto);
			var validadorHorarios = new ProfesionalHorarioValidator().ValidarHorarios(dto);
			var validadorEspecialidades = await new ProfesionalEspecialidadesValidator(_unitOfWork).ValidarEspecialidades(dto);

			if (!validacionUsuario.EsValido)
			{
				validator.Mensajes.AddRange(validacionUsuario.Mensajes);
			}
			if (!validadorHorarios.EsValido)
			{
				validator.Mensajes.AddRange(validadorHorarios.Mensajes);
			}
			if (!validadorEspecialidades.EsValido)
			{
				validator.Mensajes.AddRange(validadorEspecialidades.Mensajes);
			}
			if (await MatriculaRepetida(dto)) {
				validator.Mensajes.Add("Ya hay un profesional registrado con esa matrícula");
			}

			validator.EsValido = validator.Mensajes.Count == 0;
			return validator;
		}

		private async Task<bool> MatriculaRepetida(ProfesionalDto dto) {
			return await _unitOfWork.Profesionales
				.AnyProfesionalByMatricula(dto.Matricula);
		}
	}
}
