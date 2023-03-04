using Newtonsoft.Json;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanItem
    {
        // nested items
        [JsonProperty("item")]
        public HttpItem[] Items { get; set; } = null;

        [JsonProperty("name"), JsonRequired]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("request")]
        public HttpRequest Request { get; set; } = null;

        [JsonProperty("response")]
        public HttpResponse[] Response { get; set; } = null;
    }
}
