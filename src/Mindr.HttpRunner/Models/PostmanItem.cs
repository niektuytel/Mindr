using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mindr.HttpRunner.Models
{
    public class PostmanItem
    {
        // nested items
        [NotMapped]
        [JsonProperty("item")]
        public IEnumerable<HttpItem> Items { get; set; } = null;

        [JsonProperty("name"), JsonRequired]
        public string Name { get; set; } = "";

        [JsonProperty("description")]
        public string Description { get; set; } = "";

        [JsonProperty("request")]
        public HttpRequest Request { get; set; } = new HttpRequest();

        [JsonProperty("response")]
        public IEnumerable<HttpResponse> Response { get; set; } = new List<HttpResponse>();
    }
}
