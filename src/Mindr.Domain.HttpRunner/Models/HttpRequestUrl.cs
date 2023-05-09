using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpRequestUrl : PostmanRequestUrl
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

