using Mindr.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpVariable : PostmanVariable
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public VariablePosition Location { get; set; }
    }
}