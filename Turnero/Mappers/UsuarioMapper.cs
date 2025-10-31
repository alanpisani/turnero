using Turnero.Dto.Usuario;
using Turnero.Models;

namespace Turnero.Mappers
{
    public class UsuarioMapper
	{
		public static UsuarioDto DtoHijosAUsuarioDto(UsuarioDto dto)
		{
			var usuarioDto = new UsuarioDto(
				Nombre: dto.Nombre,
				Apellido: dto.Apellido,
				Dni: dto.Dni,
				Email: dto.Email,
				FechaNacimiento: dto.FechaNacimiento,
				Contrasenia: dto.Contrasenia,
				ContraseniaRepetida: dto.ContraseniaRepetida,
				isComplete: dto.IsComplete
				);
			return usuarioDto;
		}

		public static UsuarioRapidoDto AUsuarioRapidoDto(UsuarioRapidoDto dto) {

			return new UsuarioRapidoDto
			{
				Nombre = dto.Nombre,
				Dni = dto.Dni,
			};
		}

		public static Usuario DeDtoAUsuario(UsuarioDto dto, int idRol) {
			return new Usuario {
				Nombre= dto.Nombre,
				Apellido= dto.Apellido,
				Dni= dto.Dni,
				Email= dto.Email,
				Contrasenia= "",
				FechaNacimiento= DateOnly.Parse(dto.FechaNacimiento),
				IdRol= idRol
			};
		}

		public static Usuario DtoRapidoAUsuario(UsuarioRapidoDto dto, int idRol)
		{
			return new Usuario
			{
				Nombre = dto.Nombre,
				Dni = dto.Dni,
				IdRol = idRol
			};
		}
	}
}
