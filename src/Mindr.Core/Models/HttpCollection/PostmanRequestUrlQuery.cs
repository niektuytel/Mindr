﻿using Newtonsoft.Json;

namespace Mindr.Core.Models.HttpCollection
{
    public class PostmanRequestUrlQuery
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}