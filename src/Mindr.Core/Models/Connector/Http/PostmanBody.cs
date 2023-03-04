using Newtonsoft.Json;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanBody
    {

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("options")]
        public HttpBodyOption Options { get; set; }
    }
}



