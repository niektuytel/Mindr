using Newtonsoft.Json;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanBodyOptionRaw
    {

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}