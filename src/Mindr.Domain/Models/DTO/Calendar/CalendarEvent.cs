using System;
using System.Text.Json.Serialization;

namespace Mindr.Domain.Models.DTO.Calendar
{
    public class CalendarEvent
    {

        public CalendarEvent()
        {

        }

        public CalendarEvent(string id, string subject, DateTime? dateTimeStart, string dateTimeStartZone, DateTime? dateTimeEnd, string dateTimeEndZone, string color="#ffffff")
        {
            Id = id;
            Subject = subject;

            if(dateTimeStart != null)
            {
                StartDate = new CalendarEventDateTime()
                {
                    DateTime = (DateTime)dateTimeStart!,
                    TimeZone = dateTimeStartZone
                };
            }

            if(dateTimeEnd != null)
            {
                EndDate = new CalendarEventDateTime()
                {
                    DateTime = (DateTime)dateTimeEnd!,
                    TimeZone = dateTimeEndZone
                };
            }

            Color = color;
        }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("start")]
        public CalendarEventDateTime? StartDate { get; set; } = null;

        [JsonPropertyName("end")]
        public CalendarEventDateTime? EndDate { get; set; } = null;

        public string Color { get; set; } = "#000000";
    }

}
