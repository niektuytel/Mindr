
using Mindr.Core.Models.Connectors;
using Newtonsoft.Json;

namespace Mindr.Api.Models.Connectors
{
    public class ConnectorOverviewDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; set; }

        [JsonProperty("variables")]
        public IEnumerable<ConnectorVariable> Variables { get; set; }

    }
}