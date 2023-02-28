using Newtonsoft.Json;

namespace Mindr.Core.Models.HttpCollection
{
    public class PostmanHttpHeader
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}