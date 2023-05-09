using System.Text.Json.Serialization;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanRequestUrlQuery
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}