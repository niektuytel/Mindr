using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanVariable
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
