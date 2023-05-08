using Newtonsoft.Json;

namespace Mindr.HttpRunner.Models
{
    public class PostmanBodyOption
    {

        [JsonProperty("raw")]
        public HttpBodyOptionRaw Raw { get; set; } = new HttpBodyOptionRaw();
    }
}

