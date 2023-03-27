using Newtonsoft.Json;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanRequestUrlPath
    {
        [JsonProperty("value")]
        public string Value { get; set; }

    }
}