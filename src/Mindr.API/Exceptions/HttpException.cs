using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Mindr.Api.Models;
using System.Net;
using System.Runtime.Serialization;

namespace Mindr.Api.Exceptions
{
    [Serializable]
    internal class HttpException<TBody> : Exception
    {
        public HttpException()
        {
        }

        public HttpException(HttpStatusCode statusCode, TBody body)
        {
            StatusCode = statusCode;
            Body = body;
        }

        public HttpStatusCode StatusCode { get; set; }

        public TBody Body { get; set; }

        public ErrorMessageResponse GetErrorMessage()
        {
            return new ErrorMessageResponse((int)StatusCode, base.Message);
        }
    }
}