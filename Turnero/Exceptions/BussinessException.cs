using Turnero.Dto;

namespace Turnero.Exceptions
{
	public class BussinessException : Exception
	{
		public ResponseDto<object> Response { get; set; }

		public BussinessException(string mensaje)
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
