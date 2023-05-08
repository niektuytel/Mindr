using System.ComponentModel.DataAnnotations;
using System;
using Mindr.HttpRunner.Enums;

namespace Mindr.HttpRunner.Models
{
    public class HttpVariable : PostmanVariable
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public VariablePosition Location { get; set; }
    }
}