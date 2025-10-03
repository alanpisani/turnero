using Turnero.Dto;
using Turnero.Dto.Paciente;
using Turnero.Models;

namespace Turnero.Mappers
{
	public class PacienteMapper
	{
		public static Paciente DePacienteDtoAPaciente(PacienteDto dto, int idUsuario)
		{
			return new Paciente //Se retorna un Paciente model con el id del usuario recien creado
			(
				IdUsuario: idUsuario, //Acá iria el id del Usuario creado, subido y traido de la bd con esa intención
				Telefono: dto.Telefono //El teléfono del paciente registrado. No tiene mucha ciencia
			);
		}

		public static PacienteDtoGet DePacienteAPacienteDtoGet(Paciente paciente)
		{
			return new PacienteDtoGet
			{
				Id = paciente.IdUsuario,
				Nombre = paciente.IdUsuarioNavigation.Nombre,
				Apellido = paciente.IdUsuarioNavigation.Apellido,
				Dni = paciente.IdUsuarioNavigation.Dni,
				Email = paciente.IdUsuarioNavigation.Email,
				FechaNacimiento = paciente.IdUsuarioNavigation.FechaNacimiento.ToString(),
				Telefono = paciente.Telefono
			};
		}
		public static IEnumerable<CoberturaPaciente> CrearCoberturasPaciente(PacienteDto dto, int idPaciente)
		{
			if (dto.CoberturasMedicas == null) yield break; //Un break, por si el paciente registrado NO tiene coberturas mèdicas

			foreach (var coberturaDto in dto.CoberturasMedicas) //Se iteran todas sus coberturas (Las registradas en el form)
			{
				yield return new CoberturaPaciente //tabla intermedia CoberturaPaciente model, para asignar las relaciones
				{
					IdCoberturaMedica = coberturaDto.IdCobertura, //Las tablas intermedias requieren dos id. Uno es el de la cobertura
					IdPaciente = idPaciente, //El otro id es el del Paciente
					NumeroAfiliado = coberturaDto.NumeroAfiliado //Cada paciente tiene su numero de afiliado. Va en esta relación
				};
			}
		}
	}
}
