using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanCollection
    {

        [JsonPropertyName("info")]
        public HttpCollectionInfo Info { get; set; }

        [JsonPropertyName("item")]
        public IEnumerable<HttpItem> Items { get; set; }

        [JsonPropertyName("variable")]
        public IEnumerable<HttpVariable> Variable { get; set; }

    }
}
