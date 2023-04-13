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

        public ApiRequestException(ApiResponse code, string? message) : base(message)
        {
            ResponseCode = code;
        }

        public ApiResponse ResponseCode { get; set; }

        public ApiErrorMessage GetErrorMessage()
        {
            return new ApiErrorMessage((int)ResponseCode, Message);
        }
    }
}