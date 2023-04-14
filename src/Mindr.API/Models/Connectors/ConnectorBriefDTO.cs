
using Mindr.Core.Models.Connectors;
using Newtonsoft.Json;

namespace Mindr.Api.Models.Connectors
{
    public class ConnectorBriefDTO
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

    }
}
