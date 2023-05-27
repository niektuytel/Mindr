using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpRequest : PostmanRequest
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public ICollection<HttpVariable> Variables { get; set; } = null;

    }
}



