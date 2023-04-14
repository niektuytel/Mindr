using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.HttpRunner.Models
{
    public class HttpBody : PostmanBody
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}



