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

		public static Usuario DeDtoAUsuario(UsuarioDto dto, string rol) {
			return new Usuario {
				Nombre= dto.Nombre,
				Apellido= dto.Apellido,
				Dni= dto.Dni,
				Email= dto.Email,
				Contrasenia= "",
				FechaNacimiento= DateOnly.Parse(dto.FechaNacimiento),
				Rol= rol
			};
		}

		public static Usuario DtoRapidoAUsuario(UsuarioRapidoDto dto, string rol)
		{
			return new Usuario
			{
				Nombre = dto.Nombre,
				Dni = dto.Dni,
				Rol = rol
			};
		}
	}
}
