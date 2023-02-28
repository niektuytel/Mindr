using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Mindr.Core.Models.HttpCollection
{
    public class PostmanCollection
    {

        [JsonProperty("info")]
        public HttpCollectionInfo Info { get; set; }

        [JsonProperty("item")]
        public HttpItem[] Items { get; set; }

        [JsonProperty("variable")]
        public HttpVariable[] Variable { get; set; }

    }
}
