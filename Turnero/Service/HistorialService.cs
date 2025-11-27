using Turnero.Dto;
using Turnero.Dto.Consultum;
using Turnero.Exceptions;
using Turnero.Mappers;
using Turnero.Repositories.Interfaces;

namespace Turnero.Service
{
    public class HistorialService(IUnitOfWork unit)
    {
        private readonly IUnitOfWork _unitOfWork = unit;

        public async Task<ResponseDto<List<HistorialResponseDto>>> TraerTodosLosHistoriales()
        {
            var historialesClinicos = await _unitOfWork.HistorialesClinicos.GetAllHistoriales();

            return new ResponseDto<List<HistorialResponseDto>>
            {
                Success = true,
                Message = "Historiales clínicos traidos con éxito",
                Data = historialesClinicos.Select(h => HistorialMapper.ToDto(h)).ToList()
            };
        }

        public async Task<ResponseDto<HistorialResponseDto>> CrearHistorial(HistorialRequestDto dto)
        {
			_ = await _unitOfWork.Turnos.FindOrDefaultTurno(dto.IdTurno) ?? throw new NotFoundException("El turno no existe");

			var historialOcupado = await _unitOfWork.HistorialesClinicos.AnyHistorialByTurno(dto.IdTurno);

            if (historialOcupado) throw new BussinessException("El turno ya tiene un historial clínico relacionado.");

			await _unitOfWork.BeginTransactionAsync();

            try
            {

				var historial = HistorialMapper.ToModel(dto);

				await _unitOfWork.HistorialesClinicos.AddHistorial(historial);

				await _unitOfWork.CompleteAsync();
				await _unitOfWork.CommitAsync();

                return new ResponseDto<HistorialResponseDto>
                {
                    Success = true,
                    Message = "Historial clínico generado con éxito",
                    Data = HistorialMapper.ToDto(historial)
                };
                

            }catch(Exception e)
            {
                await _unitOfWork.RollbackAsync();

                throw new Exception($"Hubo un error inesperado al generar un historial clínico. Error: {e}");
            }
            
		}
	}
}
