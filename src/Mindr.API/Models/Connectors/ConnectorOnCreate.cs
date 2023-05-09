using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Mindr.Api.Models.Connectors
{
    public class ConnectorOnCreate
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}