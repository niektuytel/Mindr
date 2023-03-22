using Mindr.Core.Models.Connector.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    public class ConnectorVariable: PostmanVariable
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

    }
}
