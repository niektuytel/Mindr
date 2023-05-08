using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.HttpRunner.Models
{
    public class HttpBodyOption : PostmanBodyOption
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

