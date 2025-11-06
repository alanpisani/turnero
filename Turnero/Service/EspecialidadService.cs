using System.Drawing.Printing;
using Turnero.Common.Extensions;
using Turnero.Dto;
using Turnero.Dto.Especialidad;
using Turnero.Exceptions;
using Turnero.Mappers;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Service
{
	public class EspecialidadService(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;
		
		public async Task<ResponseDto<PagedResult<EspecialidadResponseDto>>> MostrarTodasLasEspecialidadesPaginadas(int pageNumber)
		{

			const int pageSize = 6;

			var query = _unitOfWork.Especialidades.Query();
			var pagedResult = await query.ToPagedResultAsync(pageNumber, pageSize);


			return new ResponseDto<PagedResult<EspecialidadResponseDto>> { 
				Success = true,
				Message = "Especialidades traidas con éxito",
				Data= new PagedResult<EspecialidadResponseDto>
				(
					pagedResult.Data.Select(e => EspecialidadMapper.toResponseDto(e)).ToList(),
					pagedResult.TotalPages,
					pagedResult.PageNumber, 
					pagedResult.PageSize
				)
			};
			
		}

		public async Task<ResponseDto<List<EspecialidadResponseDto>>> MostrarTodasLasEspecialidades()
		{

			var especialidades = await _unitOfWork.Especialidades.ToListAsyncEspecialidades();


			return new ResponseDto<List<EspecialidadResponseDto>>
			{
				Success = true,
				Message = "Especialidades traidas con éxito",
				Data = especialidades.Select(e => EspecialidadMapper.toResponseDto(e)).ToList()
			};

		}
		public async Task<ResponseDto<EspecialidadResponseDto>> MostrarEspecialidadPorId(int id)
		{

			var especialidad = await _unitOfWork.Especialidades
				.FirstOrDefaultEspecialidadById(id);

			if (especialidad == null) throw new NotFoundException("La especialidad ingresada no se encuentra en el sistema");

			return new ResponseDto<EspecialidadResponseDto> { 
				Success = true,
				Message = "Especialidad traida con éxito",
				Data= EspecialidadMapper.toResponseDto(especialidad)
			};
		}
	}
}
