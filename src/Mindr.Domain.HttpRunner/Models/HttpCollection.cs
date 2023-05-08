using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.HttpRunner.Models
{
    public class HttpCollection : PostmanCollection
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}
