using System.Text.Json.Serialization;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanRequestUrlHost
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

    }
}