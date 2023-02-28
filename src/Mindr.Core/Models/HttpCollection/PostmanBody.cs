
using Newtonsoft.Json;

namespace Mindr.Core.Models.HttpCollection
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



