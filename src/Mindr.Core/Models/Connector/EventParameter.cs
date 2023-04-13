using Mindr.Core.Enums;
using Mindr.Core.Models.Connector.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    public class EventParameter
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("type")]
        public EventType Key { get; set; } = EventType.OnDateTime;

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
