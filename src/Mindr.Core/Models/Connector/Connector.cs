using Mindr.Core.Models.Connector.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    public class Connector
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("variables")]
        public IEnumerable<ConnectorVariable> Variables { get; set; }

        [JsonProperty("pipeline")]
        public IEnumerable<HttpItem> Pipeline { get; set; }

    }
}
