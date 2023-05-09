using System.Text.Json.Serialization;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanBodyOption
    {

        [JsonPropertyName("raw")]
        public HttpBodyOptionRaw Raw { get; set; } = new HttpBodyOptionRaw();
    }
}

