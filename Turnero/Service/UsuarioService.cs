using Microsoft.AspNetCore.Identity;
using Turnero.Dto;
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

		public Usuario CrearUsuario(UsuarioDto dto, int idRol)
		{
			var usuario = UsuarioMapper.DeDtoAUsuario(dto, idRol); //Mapeamos al usuario registrado a modo Usuario BD

			usuario.Contrasenia = _passwordHasher.HashPassword(usuario, dto.Contrasenia); //HASHING

			return usuario;

		}



		//RECEPCIONISTA

		public async Task<ServiceResponse<Usuario>> RegistrarRecepcionista(UsuarioDto dto) {

			var recepcionista = CrearUsuario(dto, 3);

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				await _unitOfWork.Usuarios.AddAsyncUsuario(recepcionista);

				await _unitOfWork.CompleteAsync();
				await _unitOfWork.CommitAsync();
			}
			catch
			{
				await _unitOfWork.RollbackAsync();

				return new ServiceResponse<Usuario>()
				{
					Mensaje = "Hubo un error al registrar recepcionista. Inténtelo más tarde"
				};
			}

			return new ServiceResponse<Usuario>()
			{
				Exito = true,
				Mensaje = "Recepcionista creado con éxito",
				Cuerpo = recepcionista
			};
		}

		public async Task<ServiceResponse<List<Usuario>>> MostrarTodosLosRecepcionistas()
		{
			var recepcionistas = await _unitOfWork.Usuarios.GetAllRecepcionistas();

			if(recepcionistas.Count == 0)
			{
				return new ServiceResponse<List<Usuario>>
				{
					Mensaje = "No hay recepcionistas"
				};
			}

			return new ServiceResponse<List<Usuario>>
			{
				Exito = true,
				Cuerpo = recepcionistas
			};
		}
	}
}
