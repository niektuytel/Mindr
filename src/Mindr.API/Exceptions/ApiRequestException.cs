using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Mindr.Api.Models;
using Mindr.API.Enums;
using System.Runtime.Serialization;

namespace Mindr.API.Exceptions
{
    [Serializable]
    internal class ApiRequestException : Exception
    {
        public ApiRequestException()
        {
        }

        public ApiRequestException(Enums.ApiResponse code, string? message) : base(message)
        {
            ResponseCode = code;
        }

        public Enums.ApiResponse ResponseCode { get; set; }

        public Api.Models.ErrorMessageResponse GetErrorMessage()
        {
            return new Api.Models.ErrorMessageResponse((int)ResponseCode, base.Message);
        }
    }
}