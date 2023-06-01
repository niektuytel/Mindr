using Mindr.Domain.Models.DTO.Connector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mindr.Domain.Models.DTO.Calendar
{
    public class CalendarAppointment
    {

        public CalendarAppointment()
        {

        }

        public CalendarAppointment(string id, string subject, DateTime? dateTimeStart, string dateTimeStartZone, DateTime? dateTimeEnd, string dateTimeEndZone, IEnumerable<ConnectorEvent> connectorEvents, string color="#ffffff")
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
            ConnectorEvents = connectorEvents;
        }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("start")]
        public CalendarEventDateTime? StartDate { get; set; } = null;

        [JsonPropertyName("end")]
        public CalendarEventDateTime? EndDate { get; set; } = null;

        [JsonPropertyName("connector_events")]
        public IEnumerable<ConnectorEvent> ConnectorEvents { get; set; } = new List<ConnectorEvent>();

        public string Color { get; set; } = "#000000";
    }

}