using Mindr.Shared.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mindr.Shared.Models.ConnectorEvents
{
    public class ConnectorEventParameter
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("type")]
        public EventType Key { get; set; } = EventType.OnDateTime;

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
