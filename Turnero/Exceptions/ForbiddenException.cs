using Turnero.Dto;

namespace Turnero.Exceptions
{
	public class ForbiddenException: Exception
	{
		public ResponseDto<object> Response { get; set; }

		public ForbiddenException(string mensaje)
			: base(mensaje)
		{
			Response = new ResponseDto<object>
			{
				Success = false,
				Message = mensaje,

			};
		}
	}
}
