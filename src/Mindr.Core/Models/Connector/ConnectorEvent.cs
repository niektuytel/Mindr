using Mindr.Core.Models.Connector.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    public class ConnectorEvent
    {
        public Guid Id { get; set; }
        public bool Recurring { get; set; }
        public PostmanVariable[] Input { get; set; }
        //public CollectionBriefDTO Collection { get; set; }

    }
}
