using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mindr.Domain.Models.DTO.Connector
{
    public class ConnectorEvent
    {
        public ConnectorEvent()
        {

        }

        public ConnectorEvent(string userId, string eventId, Connector connector)
        {
            UserId = userId;
            EventId = eventId;
            ConnectorId = connector.Id;
            ConnectorName = connector.Name;
            ConnectorVariables = connector.Variables.Where(item => item.IsPublic).ToArray();
        }

        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = "";

        [JsonPropertyName("job_id")]
        public string JobId { get; set; } = "";

        [JsonPropertyName("event_id")]
        public string EventId { get; set; }

        [JsonPropertyName("event_parameters")]
        public IEnumerable<ConnectorEventVariable> EventParameters { get; set; } = new List<ConnectorEventVariable>();

        [JsonPropertyName("connector_id")]
        public Guid? ConnectorId { get; set; } = null;

        [JsonPropertyName("connector_name")]
        public string ConnectorName { get; set; } = "";

        [JsonPropertyName("connector_variables")]
        public IEnumerable<ConnectorVariable> ConnectorVariables { get; set; } = new List<ConnectorVariable>();

        [JsonPropertyName("connector_color")]
        public string ConnectorColor { get; set; } = "#000000";

    }
}
