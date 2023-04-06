using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanRequest
    {
        [JsonProperty("method")]
        public string Method { get; set; } = "";

        [JsonProperty("header")]
        public IEnumerable<HttpHeader> Header { get; set; } = new List<HttpHeader>();

        [JsonProperty("body")]
        public HttpBody Body { get; set; } = new HttpBody();

        [JsonProperty("url")]
        public HttpRequestUrl Url { get; set; } = new HttpRequestUrl();

    }
}

