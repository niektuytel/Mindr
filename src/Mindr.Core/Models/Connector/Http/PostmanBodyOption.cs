using Newtonsoft.Json;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanBodyOption
    {

        [JsonProperty("raw")]
        public HttpBodyOptionRaw Raw { get; set; }
    }
}

