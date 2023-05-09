using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanVariable
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
