using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpBodyOptionRaw : PostmanBodyOptionRaw
    {
        public HttpBodyOptionRaw()
        { }

        public HttpBodyOptionRaw(Guid id, HttpBodyOptionRaw entity)
        {
            Id = id;
            Language = entity.Language;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }

}


