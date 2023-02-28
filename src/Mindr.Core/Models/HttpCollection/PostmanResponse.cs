﻿
using Newtonsoft.Json;

namespace Mindr.Core.Models.HttpCollection
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
        public object Header { get; set; }

        [JsonProperty("cookie")]
        public object[] Cookie { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
