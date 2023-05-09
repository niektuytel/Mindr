using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpBodyOptionRaw : PostmanBodyOptionRaw
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }

}


