using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Mindr.Api.Models;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;

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
            var code = (int)StatusCode;
            var type = typeof(TBody).Name;
            if(type == typeof(string).Name)
            {
                return new ErrorMessageResponse(code, type, Body as string);
            }

            var content = JsonSerializer.Serialize(Body);
            return new ErrorMessageResponse(code, type, content);
        }
    }
}