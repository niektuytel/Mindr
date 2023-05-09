using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpRequestUrlPath : PostmanRequestUrlPath
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

