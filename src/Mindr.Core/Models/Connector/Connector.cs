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

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("variables")]
        public IList<ConnectorVariable> Variables { get; set; } = new List<ConnectorVariable>();

        [JsonProperty("pipeline")]
        public IEnumerable<HttpItem> Pipeline { get; set; } = new List<HttpItem>();

    }
}
