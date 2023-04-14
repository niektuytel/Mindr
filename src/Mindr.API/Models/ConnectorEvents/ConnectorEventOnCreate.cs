using Microsoft.Graph;
using Mindr.Core.Models.ConnectorEvents;
using Mindr.Core.Models.Connectors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mindr.Api.Models.ConnectorEvents
{
    public class ConnectorEventOnCreate
    {
        [JsonProperty("event_id")]
        public string EventId { get; set; }

        [JsonProperty("event_params")]
        public IEnumerable<ConnectorEventParameter> EventParameters { get; set; } = new List<ConnectorEventParameter>();

        [JsonProperty("connector_id")]
        public Guid? ConnectorId { get; set; } = null;

        [JsonProperty("connector_variables")]
        public IEnumerable<ConnectorVariable> ConnectorVariables { get; set; } = new List<ConnectorVariable>();

        public ConnectorEvent ToConnectorEvent(string userId, Connector connector)
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