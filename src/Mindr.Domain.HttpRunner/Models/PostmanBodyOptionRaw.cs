using System.Text.Json.Serialization;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanBodyOptionRaw
    {

        [JsonPropertyName("language")]
        public string Language { get; set; }
    }
}