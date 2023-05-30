using System.Text.Json.Serialization;
using System.Collections;
using System.Collections.Generic;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("originalRequest")]
        public HttpRequest OriginalRequest { get; set; } = new HttpRequest();

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("_postman_previewlanguage")]
        public string PostmanPreviewLanguage { get; set; }

        [JsonPropertyName("header")]
        public IEnumerable<HttpHeader> Header { get; set; } = new List<HttpHeader>();

        [JsonPropertyName("cookie")]
        public IEnumerable<HttpCookie> Cookie { get; set; } = new List<HttpCookie>();

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}
