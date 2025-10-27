using Turnero.Dto;

namespace Turnero.Exceptions
{
    public class NotFoundException : Exception
    {
        public ResponseDto<object> Response { get; set; }

        public NotFoundException(string mensaje)
            : base(mensaje)
        {
            Response = new ResponseDto<object> {
                Success = false,
                Message= mensaje,

            };
        }

    }
}
