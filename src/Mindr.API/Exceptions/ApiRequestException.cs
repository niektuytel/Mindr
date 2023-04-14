using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Mindr.Api.Models;
using System.Net;
using System.Runtime.Serialization;

namespace Mindr.API.Exceptions
{
    [Serializable]
    internal class ApiRequestException : Exception
    {
        public ApiRequestException()
        {
        }

        public ApiRequestException(HttpStatusCode code, string? message) : base(message)
        {
            ResponseCode = code;
        }

        public HttpStatusCode ResponseCode { get; set; }

        public Api.Models.ErrorMessageResponse GetErrorMessage()
        {
            return new Api.Models.ErrorMessageResponse((int)ResponseCode, base.Message);
        }
    }
}