using Newtonsoft.Json;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanHttpHeader
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}