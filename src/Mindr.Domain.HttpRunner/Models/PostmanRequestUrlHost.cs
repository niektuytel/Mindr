using Newtonsoft.Json;

namespace Mindr.HttpRunner.Models
{
    public class PostmanRequestUrlHost
    {
        [JsonProperty("value")]
        public string Value { get; set; }

    }
}