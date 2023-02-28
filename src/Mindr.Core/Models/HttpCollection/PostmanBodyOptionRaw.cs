
using Newtonsoft.Json;

namespace Mindr.Core.Models.HttpCollection
{
    public class PostmanBodyOptionRaw
    {

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}