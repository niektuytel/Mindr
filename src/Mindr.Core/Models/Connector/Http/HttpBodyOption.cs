using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpBodyOption : PostmanBodyOption
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

