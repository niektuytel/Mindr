using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanItem
    {
        // nested items
        [NotMapped]
        [JsonPropertyName("item")]
        public IEnumerable<HttpItem> Items { get; set; } = null;

        [JsonPropertyName("name"), JsonInclude]
        public string Name { get; set; } = "";

        [JsonPropertyName("description")]
        public string Description { get; set; } = "";

        [JsonPropertyName("request")]
        public HttpRequest Request { get; set; } = new HttpRequest();

        [JsonPropertyName("response")]
        public IEnumerable<HttpResponse> Response { get; set; } = new List<HttpResponse>();
    }
}
