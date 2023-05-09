using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpBody : PostmanBody
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}



