﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanRequestUrl
    {

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        // TODO: Use No-SQL database
        [NotMapped]
        [JsonProperty("host")]
        public string[] Host { get; set; }

        // TODO: Use No-SQL database
        [NotMapped]
        [JsonProperty("path")]
        public string[] Path { get; set; }

        [JsonProperty("query")]
        public HttpRequestUrlQuery[] Query { get; set; }

    }
}