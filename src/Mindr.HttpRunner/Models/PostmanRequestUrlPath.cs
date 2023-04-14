using Newtonsoft.Json;

namespace Mindr.HttpRunner.Models
{
    public class PostmanRequestUrlPath
    {
        [JsonProperty("value")]
        public string Value { get; set; }

    }
}