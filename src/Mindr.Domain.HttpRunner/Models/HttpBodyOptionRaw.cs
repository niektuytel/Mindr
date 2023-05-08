using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.HttpRunner.Models
{
    public class HttpBodyOptionRaw : PostmanBodyOptionRaw
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }

}


