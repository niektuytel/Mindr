using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Mindr.Api.Models;
using System.Net;
using System.Runtime.Serialization;

namespace Mindr.API.Exceptions
{
    [Serializable]
    internal class HttpException : Exception
    {
        public HttpException()
        {
        }

        public HttpException(HttpStatusCode code, string? message) : base(message)
        {
            StatusCode = code;
        }

        public HttpStatusCode StatusCode { get; set; }

        public ErrorMessageResponse GetErrorMessage()
        {
            return new ErrorMessageResponse((int)StatusCode, base.Message);
        }
    }
}