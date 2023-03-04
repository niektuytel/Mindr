using Newtonsoft.Json;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanRequest
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("header")]
        public HttpHeader[] Header { get; set; }

        [JsonProperty("body")]
        public HttpBody Body { get; set; }

        [JsonProperty("url")]
        public HttpRequestUrl Url { get; set; }

    }
}

