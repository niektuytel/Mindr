using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.HttpRunner.Models
{
    public class HttpRequestUrlPath : PostmanRequestUrlPath
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

