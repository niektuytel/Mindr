using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace Mindr.HttpRunner.Models
{
    public class PostmanResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("originalRequest")]
        public HttpRequest OriginalRequest { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("_postman_previewlanguage")]
        public string PostmanPreviewLanguage { get; set; }

        [JsonProperty("header")]
        public IEnumerable<HttpHeader> Header { get; set; } = new List<HttpHeader>();

        [JsonProperty("cookie")]
        public IEnumerable<HttpCookie> Cookie { get; set; } = new List<HttpCookie>();

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
