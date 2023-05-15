using System.Text.Json.Serialization;
using System;

namespace Mindr.Domain.Models.DTO.Calendar
{
    public class CalendarEventDateTime
    {
        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; }
    }
}