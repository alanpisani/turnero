using Microsoft.AspNetCore.Identity;
using Turnero.Common.Enums;
using Turnero.Dto;
using Turnero.Dto.Usuario;
using Turnero.Mappers;
using Turnero.Models;
using Turnero.Repositories.Interfaces;
using Turnero.Validators.UsuarioValidators;

namespace Turnero.Service
{
    public class UsuarioService(IUnitOfWork unit)
	{
		private readonly PasswordHasher<Usuario> _passwordHasher = new();
		private readonly IUnitOfWork _unitOfWork = unit;

		

		public async Task<ResponseDto<List<UsuarioResponseDto>>> ConsultarUsuarios()
		{
			var usuarios = await _unitOfWork.Usuarios.GetAll();

			return new ResponseDto<List<UsuarioResponseDto>> { 
				Success = true,
				Message = "Usuarios consultados con éxito",
				Data = usuarios.Select(u => UsuarioMapper.ToUsuarioDto(u)).ToList()
			};
		}

		public Usuario CrearUsuario(UsuarioRequestDto dto, string rol)
		{
			var usuario = UsuarioMapper.DeDtoAUsuario(dto, rol); //Mapeamos al usuario registrado a modo Usuario BD

			usuario.Password = _passwordHasher.HashPassword(usuario, dto.Contrasenia); //HASHING

			return usuario;

		}

		public Usuario CrearUsuarioRapido(UsuarioRapidoDto dto, string rol)
		{
			var usuario = UsuarioMapper.DtoRapidoAUsuario(dto, rol); //Mapeamos al usuario registrado a modo Usuario BD

			return usuario;

		}



		//RECEPCIONISTA

		public async Task<Usuario> RegistrarRecepcionista(UsuarioRequestDto dto) {

			var recepcionista = CrearUsuario(dto, RolesUsuario.Recepcionista.ToString());

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				await _unitOfWork.Usuarios.AddAsyncUsuario(recepcionista);

				await _unitOfWork.CompleteAsync();
				await _unitOfWork.CommitAsync();

				return recepcionista;
			}
			catch
			{
				await _unitOfWork.RollbackAsync();

				throw new Exception("Hubo un error al registrar recepcionista. Inténtelo más tarde");
			}
		}

		public async Task<List<Usuario>> MostrarTodosLosRecepcionistas()
		{
			var recepcionistas = await _unitOfWork.Usuarios.GetAllRecepcionistas();


			return recepcionistas;
		}
	}
}
