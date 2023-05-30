using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpBodyOption : PostmanBodyOption
    {
        public HttpBodyOption()
        { }

        public HttpBodyOption(Guid id, HttpBodyOption entity)
        {
            Id = id;
            Raw = id == entity.Id ? new HttpBodyOptionRaw(entity.Raw.Id, entity.Raw) : new HttpBodyOptionRaw(Guid.NewGuid(), entity.Raw);
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

