using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Mindr.Domain.Models.DTO.Connector
{
    public class ConnectorBriefDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("variables")]
        public IEnumerable<ConnectorVariable> Variables { get; set; }

    }
}
