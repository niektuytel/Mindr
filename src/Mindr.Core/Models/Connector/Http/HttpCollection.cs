using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpCollection : PostmanCollection
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}
