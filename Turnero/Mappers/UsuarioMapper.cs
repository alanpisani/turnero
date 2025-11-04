using Turnero.Dto.Usuario;
using Turnero.Models;

namespace Turnero.Mappers
{
    public class UsuarioMapper
	{
		public static UsuarioRequestDto DtoHijosAUsuarioDto(UsuarioRequestDto dto)
		{
			var usuarioDto = new UsuarioRequestDto(
				Nombre: dto.Nombre,
				Apellido: dto.Apellido,
				Dni: dto.Dni,
				Email: dto.Email,
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

		public static Usuario DeDtoAUsuario(UsuarioRequestDto dto, string rol) {
			return new Usuario {
				Nombre= dto.Nombre,
				Apellido= dto.Apellido,
				Dni= dto.Dni,
				Email= dto.Email,
				Password= "",
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

		public static UsuarioResponseDto ToUsuarioDto(Usuario usuario)
		{
			return new UsuarioResponseDto {
				Nombre = usuario.Nombre,
				Apellido = usuario.Apellido,
				Dni= usuario.Dni,
				Email = usuario.Email,
				Rol= usuario.Rol,
				IsComplete = usuario.IsComplete

			};
		}
	}
}
