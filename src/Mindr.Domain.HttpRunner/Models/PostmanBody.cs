

using System.Text.Json.Serialization;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanBody
    {

        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("options")]
        public HttpBodyOption Options { get; set; } = new HttpBodyOption();
    }
}



