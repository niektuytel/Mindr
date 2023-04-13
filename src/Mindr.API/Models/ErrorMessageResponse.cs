using Mindr.API.Enums;

namespace Mindr.Api.Models
{
    public class ErrorMessageResponse
    {
        public ErrorMessageResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; }
        public string Message { get; }
    }
}