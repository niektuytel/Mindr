using Mindr.Core.Models.Connectors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mindr.Core.Models.ConnectorEvents
{
    public class ConnectorEvent
    {
        private Connector connector;

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
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("user_id")]
        public string UserId { get; set; } = "";

        [JsonProperty("job_id")]
        public string JobId { get; set; } = "";

        [JsonProperty("event_id")]
        public string EventId { get; set; }

        [JsonProperty("event_parameters")]
        public IEnumerable<ConnectorEventParameter> EventParameters { get; set; }

        [JsonProperty("connector_id")]
        public Guid? ConnectorId { get; set; } = null;

        [JsonProperty("connector_name")]
        public string ConnectorName { get; set; } = "";

        [JsonProperty("connector_variables")]
        public IEnumerable<ConnectorVariable> ConnectorVariables { get; set; } = new List<ConnectorVariable>();

        [JsonProperty("connector_color")]
        public string ConnectorColor { get; set; } = "#000000";

    }
}
