using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpHeader : PostmanHttpHeader
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}


