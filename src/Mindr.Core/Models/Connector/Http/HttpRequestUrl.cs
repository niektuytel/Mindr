using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpRequestUrl : PostmanRequestUrl
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

