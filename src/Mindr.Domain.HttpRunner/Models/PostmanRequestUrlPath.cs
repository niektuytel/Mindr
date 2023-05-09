using System.Text.Json.Serialization;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanRequestUrlPath
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

    }
}