using System;
using System.Text.Json.Serialization;

namespace Mindr.Core.Models
{
    public class AgendaEvent
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("start")]
        public AgendaEventDateTime StartDate { get; set; }

        [JsonPropertyName("end")]
        public AgendaEventDateTime EndDate { get; set; }

        public string Color { get; set; } = "#000000";
    }
}