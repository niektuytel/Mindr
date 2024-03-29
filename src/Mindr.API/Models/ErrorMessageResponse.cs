﻿namespace Mindr.Api.Models
{
    public class ErrorMessageResponse
    {
        public ErrorMessageResponse(int code, string type, string content)
        {
            Code = code;
            Type = type;
            Content = content;
        }

        public int Code { get; }

        public string Type { get; }

        public string Content { get; }
    }
}