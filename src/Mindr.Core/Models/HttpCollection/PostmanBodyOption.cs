
using Newtonsoft.Json;

namespace Mindr.Core.Models.HttpCollection
{
    public class PostmanBodyOption
    {

        [JsonProperty("raw")]
        public HttpBodyOptionRaw Raw { get; set; }
    }
}

