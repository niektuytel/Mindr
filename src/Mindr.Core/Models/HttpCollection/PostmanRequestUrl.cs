
using Newtonsoft.Json;

namespace Mindr.Core.Models.HttpCollection
{
    public class PostmanRequestUrl
    {

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("host")]
        public string[] Host { get; set; }

        [JsonProperty("path")]
        public string[] Path { get; set; }

        [JsonProperty("query")]
        public HttpRequestUrlQuery[] Query { get; set; }

    }
}