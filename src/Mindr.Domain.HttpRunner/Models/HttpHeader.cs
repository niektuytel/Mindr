using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpHeader : PostmanHttpHeader
    {
        public HttpHeader()
        { }

        public HttpHeader(Guid id, HttpHeader entity)
        {
            Id = id;
            Key = entity.Key;
            Value = entity.Value;
            Type = entity.Type;
            Description = entity.Description;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}


