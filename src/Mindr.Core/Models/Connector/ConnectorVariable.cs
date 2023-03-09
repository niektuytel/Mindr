using Mindr.Core.Models.Connector.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    public class ConnectorVariable: PostmanVariable
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
