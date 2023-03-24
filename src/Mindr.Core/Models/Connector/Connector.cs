using Mindr.Core.Models.Connector.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    /// <summary>
    /// Used to execute pipeline with etc.
    /// </summary>
    public class Connector
    {
        public Connector() { }

        [Key]
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("variables")]
        public IEnumerable<ConnectorParam> Variables { get; set; }

        [JsonProperty("pipeline")]
        public IEnumerable<HttpItem> Pipeline { get; set; }

    }
}
