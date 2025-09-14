using Turnero.Dto;
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
				ContraseniaRepetida: dto.ContraseniaRepetida
				);
			return usuarioDto;
		}

		public static Usuario DeDtoAUsuario(UsuarioDto dto, int idRol) {
			return new Usuario(
				Nombre: dto.Nombre,
				Apellido: dto.Apellido,
				Dni: dto.Dni,
				Email: dto.Email,
				Contrasenia: "",
				FechaNacimiento: DateOnly.Parse(dto.FechaNacimiento),
				IdRol: idRol
			);
		}
	}
}
