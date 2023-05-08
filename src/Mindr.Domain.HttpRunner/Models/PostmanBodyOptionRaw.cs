using Newtonsoft.Json;

namespace Mindr.HttpRunner.Models
{
    public class PostmanBodyOptionRaw
    {

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}