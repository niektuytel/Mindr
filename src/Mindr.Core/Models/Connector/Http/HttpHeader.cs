using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpHeader : PostmanHttpHeader
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}


