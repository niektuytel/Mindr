using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpBody : PostmanBody
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}



