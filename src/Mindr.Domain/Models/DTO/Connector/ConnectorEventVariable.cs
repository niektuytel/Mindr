using Mindr.Domain.Enums;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mindr.Domain.Models.DTO.Connector
{
    public class ConnectorEventVariable
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("type")]
        public EventType Key { get; set; } = EventType.OnDateTime;

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
