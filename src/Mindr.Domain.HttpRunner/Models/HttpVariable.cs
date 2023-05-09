using System.ComponentModel.DataAnnotations;
using System;
using Mindr.Domain.HttpRunner.Enums;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpVariable : PostmanVariable
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public VariablePosition Location { get; set; }
    }
}