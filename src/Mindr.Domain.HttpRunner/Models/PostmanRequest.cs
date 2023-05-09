using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanRequest
    {
        [JsonPropertyName("method")]
        public string Method { get; set; } = "";

        [JsonPropertyName("header")]
        public IEnumerable<HttpHeader> Header { get; set; } = new List<HttpHeader>();

        [JsonPropertyName("body")]
        public HttpBody Body { get; set; } = new HttpBody();

        [JsonPropertyName("url")]
        public HttpRequestUrl Url { get; set; } = new HttpRequestUrl();

    }
}

