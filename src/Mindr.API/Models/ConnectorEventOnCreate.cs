using Microsoft.Graph;
using Mindr.Core.Models.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mindr.Api.Models
{
    public class ConnectorEventOnCreate
    {
        [JsonProperty("event_id")]
        public string EventId { get; set; }

        [JsonProperty("event_params")]
        public IEnumerable<EventParameter> EventParameters { get; set; } = new List<EventParameter>();

        [JsonProperty("connector_id")]
        public Guid? ConnectorId { get; set; } = null;

        [JsonProperty("connector_variables")]
        public IEnumerable<ConnectorVariable> ConnectorVariables { get; set; } = new List<ConnectorVariable>();

        public ConnectorEvent NewConnectorEvent(string userId, Connector connector)
        {
            var item = new ConnectorEvent()
            {
                UserId = userId,
                EventId = EventId,
                EventParameters = EventParameters,
                ConnectorVariables = ConnectorVariables,
                ConnectorId = connector.Id,
                ConnectorName = connector.Name,
                ConnectorColor = connector.Color
            };

            return item;
        }

    }
}