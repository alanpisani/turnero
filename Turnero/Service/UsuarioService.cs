using System.Drawing.Printing;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Turnero.Common.Enums;
using Turnero.Common.Extensions;
using Turnero.Dto;
using Turnero.Dto.Usuario;
using Turnero.Exceptions;
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

		public async Task<ResponseDto<PagedResult<UsuarioResponseDto>>>  ConsultarUsuarios(int pageNumber)
		{
			const int pageSize = 6;

			var query = _unitOfWork.Usuarios.Query(); // método que devuelva IQueryable<Usuario>

			var pagedResult = await query.ToPagedResultAsync(pageNumber, pageSize);

			return new ResponseDto<PagedResult<UsuarioResponseDto>>
			{
				Success = true,
				Message = "Usuarios consultados con éxito",
				Data = new PagedResult<UsuarioResponseDto>(
					pagedResult.Data
					.Select(u => UsuarioMapper.ToUsuarioDto(u))
					.Where(u => u.Rol != RolesUsuario.Admin.ToString())
					.ToList(),
					pagedResult.TotalRecords,
					pagedResult.PageNumber,
					pagedResult.PageSize
				)
			};
		}

		public Usuario CrearUsuario(UsuarioRequestDto dto, string rol)
		{
			var usuario = UsuarioMapper.DeDtoAUsuario(dto, rol); //Mapeamos al usuario registrado a modo Usuario BD

			usuario.Password = _passwordHasher.HashPassword(usuario, dto.Contrasenia); //HASHING

			return usuario;

		}

		public string Hashear(Usuario usuario, string password)
		{
			return _passwordHasher.HashPassword(usuario, password);
		}

		public Usuario CrearUsuarioRapido(UsuarioRapidoDto dto, string rol)
		{
			var usuario = UsuarioMapper.DtoRapidoAUsuario(dto, rol); //Mapeamos al usuario registrado a modo Usuario BD

			return usuario;

		}

		public async Task<ResponseDto<UsuarioResponseDto>> CambiarEstadoUsuario(int idUsuario, bool estado)
		{
			var usuario = await _unitOfWork.Usuarios.FirstOrDefaultUsuario(idUsuario);

			if (usuario == null) throw new NotFoundException("El usuario no se encuentra registrado en el sistema");

			usuario!.IsActive = estado;

			await _unitOfWork.CompleteAsync();
			await _unitOfWork.CommitAsync();

			return new ResponseDto<UsuarioResponseDto> { 
			
				Success = true,
				Message = $"Usuario {(usuario.IsActive ? "Activado" : "desactivado")} con éxito",
				Data = UsuarioMapper.ToUsuarioDto(usuario)
			};

		}



		//RECEPCIONISTA

		public async Task<ResponseDto<UsuarioResponseDto>> RegistrarRecepcionista(UsuarioRequestDto dto) {

			var recepcionista = CrearUsuario(dto, RolesUsuario.Recepcionista.ToString());

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				await _unitOfWork.Usuarios.AddAsyncUsuario(recepcionista);

				await _unitOfWork.CompleteAsync();
				await _unitOfWork.CommitAsync();

				return new ResponseDto<UsuarioResponseDto> {
					Success= true,
					Message= "Recepcionista creado con éxito",
					Data= UsuarioMapper.ToUsuarioDto(recepcionista)
				};
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
