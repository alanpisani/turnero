using Turnero.Dto.Paciente;
using Turnero.Models;

namespace Turnero.Mappers
{
    public class PacienteMapper
	{
		public static Paciente DePacienteDtoAPaciente(PacienteRequestDto dto, int idUsuario)
		{
			return new Paciente //Se retorna un Paciente model con el id del usuario recien creado
			{
				IdUsuario= idUsuario, //Acá iria el id del Usuario creado, subido y traido de la bd con esa intención
				Telefono= dto.Telefono //El teléfono del paciente registrado. No tiene mucha ciencia
			};
		}

		public static PacienteResponseDto ToDto(Paciente paciente)
		{
			return new PacienteResponseDto
			{
				Id = paciente.IdUsuario,
				Nombre = paciente.IdUsuarioNavigation.Nombre,
				Apellido = paciente.IdUsuarioNavigation.Apellido,
				Dni = paciente.IdUsuarioNavigation.Dni,
				Email = paciente.IdUsuarioNavigation.Email,
				Telefono = paciente.Telefono
			};
		}
	}
}
