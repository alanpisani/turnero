using Turnero.Dto;
using Turnero.Dto.Consultum;
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
    }
}
