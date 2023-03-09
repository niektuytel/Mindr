using Mindr.Core.Models.Connector.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Mindr.Core.Models.Connector
{
    public class ConnectorBriefDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("variables")]
        public IEnumerable<ConnectorVariable> Variables { get; set; }

    }
}
