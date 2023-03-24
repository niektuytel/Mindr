using Mindr.Core.Enums;
using Mindr.Core.Models.Connector.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    public class EventParam
    {
        [Key]
        public int Id { get; set; }

        [JsonProperty("type")]
        public EventType Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
