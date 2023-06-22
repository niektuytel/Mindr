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

        public CalendarAppointment(CalendarAppointment data, CalendarEventDateTime start, CalendarEventDateTime end, string color = "#ffffff")
        {
            Id = data.Id;
            CalendarId = data.CalendarId;
            Subject = data.Subject;
            StartDate = start;
            EndDate = end;
            ConnectorEvents = data.ConnectorEvents;
            Color = color;
        }

        public CalendarAppointment(string id, string calendarId, string subject, CalendarEventDateTime start, CalendarEventDateTime end, IEnumerable<ConnectorEvent> connectorEvents, string color="#ffffff")
        {
            Id = id;
            CalendarId = calendarId;
            Subject = subject;
            StartDate = start;
            EndDate = end;
            Color = color;
            ConnectorEvents = connectorEvents;
        }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("calendar_id")]
        public string CalendarId { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("start")]
        public CalendarEventDateTime StartDate { get; set; }

        [JsonPropertyName("end")]
        public CalendarEventDateTime EndDate { get; set; }

        [JsonPropertyName("connector_events")]
        public IEnumerable<ConnectorEvent> ConnectorEvents { get; set; } = new List<ConnectorEvent>();

        public string Color { get; set; } = "#000000";
    }

}
