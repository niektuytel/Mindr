using Mindr.Core.Models.Connector.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    public class ConnectorVariable: PostmanVariable
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool InputByUser { get; set; } = true;

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
