using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Mindr.Domain.Models.DTO.Connector
{
    public class ConnectorOverviewDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; } = false;

        [JsonPropertyName("variables")]
        public IEnumerable<ConnectorVariable> Variables { get; set; }

    }
}