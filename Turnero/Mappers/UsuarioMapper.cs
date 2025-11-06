using Turnero.Dto.Usuario;
using Turnero.Models;

namespace Turnero.Mappers
{
    public class UsuarioMapper
	{
		public static UsuarioResponseDto DtoHijosAUsuarioDto(UsuarioResponseDto dto)
		{
			var usuarioDto = new UsuarioResponseDto {
				IdUsuario = dto.IdUsuario,
				Nombre= dto.Nombre,
				Apellido= dto.Apellido,
				Dni= dto.Dni,
				Email= dto.Email,
				IsActive= dto.IsActive
				};
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
				IdUsuario = usuario.IdUsuario,
				Nombre = usuario.Nombre,
				Apellido = usuario.Apellido,
				Dni= usuario.Dni,
				Email = usuario.Email,
				Rol= usuario.Rol,
				IsActive = usuario.IsActive

			};
		}
	}
}
