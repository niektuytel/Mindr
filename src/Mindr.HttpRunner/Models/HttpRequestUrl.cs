using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.HttpRunner.Models
{
    public class HttpRequestUrl : PostmanRequestUrl
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

