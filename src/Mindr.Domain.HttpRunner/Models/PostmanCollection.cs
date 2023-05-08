using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Mindr.HttpRunner.Models
{
    public class PostmanCollection
    {

        [JsonProperty("info")]
        public HttpCollectionInfo Info { get; set; }

        [JsonProperty("item")]
        public IEnumerable<HttpItem> Items { get; set; }

        [JsonProperty("variable")]
        public IEnumerable<HttpVariable> Variable { get; set; }

    }
}
