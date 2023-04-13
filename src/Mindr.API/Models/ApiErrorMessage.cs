using Mindr.API.Enums;

namespace Mindr.Api.Models
{
    public class ApiErrorMessage
    {
        public ApiErrorMessage(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; }
        public string Message { get; }
    }
}