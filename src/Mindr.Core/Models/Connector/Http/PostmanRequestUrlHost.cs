using Newtonsoft.Json;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanRequestUrlHost
    {
        [JsonProperty("value")]
        public string Value { get; set; }

    }
}