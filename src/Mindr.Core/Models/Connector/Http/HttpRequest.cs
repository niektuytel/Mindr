using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpRequest : PostmanRequest
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public IEnumerable<HttpVariable> Variables { get; set; } = null;

    }
}



