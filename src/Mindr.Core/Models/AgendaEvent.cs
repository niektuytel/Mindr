using Mindr.Core.Models.GoogleCalendar;
using System;
using System.Text.Json.Serialization;

namespace Mindr.Core.Models
{
    public class AgendaEvent
    {

        public AgendaEvent()
        {
            
        }

        public AgendaEvent(GoogleCalendarEvents.Item item)
        {
            Id = item.id;
            Subject = item.summary;
            StartDate = new AgendaEventDateTime()
            {
                DateTime = item.start.dateTime,
                TimeZone = item.start.timeZone
            };
            EndDate = new AgendaEventDateTime()
            {
                DateTime = item.end.dateTime,
                TimeZone = item.end.timeZone
            };
            // Color = 
        }

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