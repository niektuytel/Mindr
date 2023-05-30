using System.ComponentModel.DataAnnotations;
using System;
using Mindr.Domain.HttpRunner.Enums;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpVariable : PostmanVariable
    {
        public HttpVariable()
        { }

        public HttpVariable(Guid id, HttpVariable variable)
        {
            Id = id;
            Location = variable.Location;
            Key = variable.Key;
            Value = variable.Value;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public VariablePosition Location { get; set; }
    }
}