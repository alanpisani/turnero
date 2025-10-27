using Turnero.Dto;
using Turnero.Dto.TurnoDto;

namespace Turnero.Exceptions.Turno
{
    public class NoTurnoException: Exception
	{
		public ResponseDto<List<TurnoRequestDto>> Response { get; set; }

		public NoTurnoException(string mensaje)
		: base(mensaje)
			{
				Response= new ResponseDto<List<TurnoRequestDto>>
				{
					Success=true,
					Message= mensaje,
					Data= new List<TurnoRequestDto>()
				};
			}
	}
}
