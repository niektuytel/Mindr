using Mindr.Core.Models.Connector.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    public class ConnectorEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public ConnectorEventInput[] Input { get; set; }
        public ConnectorBriefDTO Connector { get; set; }

    }
}
