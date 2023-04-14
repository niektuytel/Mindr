using Newtonsoft.Json;

namespace Mindr.HttpRunner.Models
{
    public class PostmanBody
    {

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("options")]
        public HttpBodyOption Options { get; set; } = new HttpBodyOption();
    }
}



