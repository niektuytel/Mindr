
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace Mindr.HttpRunner.Models
{
    public class HttpResponse : PostmanResponse
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public IEnumerable<HttpVariable> Variables { get; set; } = new List<HttpVariable>();
    }
}

