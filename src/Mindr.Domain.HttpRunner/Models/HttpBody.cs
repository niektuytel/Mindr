using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpBody : PostmanBody
    {
        public HttpBody()
        { }

        public HttpBody(Guid id, HttpBody entity)
        {
            Id = id;
            Mode = entity.Mode;
            Raw = entity.Raw;
            Options = id == entity.Id ? new HttpBodyOption(entity.Options.Id, entity.Options) : new HttpBodyOption(Guid.NewGuid(), entity.Options);
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}



