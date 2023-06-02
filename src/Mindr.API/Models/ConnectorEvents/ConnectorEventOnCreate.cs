using Microsoft.Graph;
using Mindr.Domain.Models.DTO.Connector;

using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using Mindr.Domain.Models.DTO.Connector;

namespace Mindr.Api.Models.ConnectorEvents
{
    public class ConnectorEventOnCreate
    {
        public ConnectorEventOnCreate()
        { }

        public ConnectorEventOnCreate(ConnectorEvent connectorEvent)
        {
            EventId = connectorEvent.EventId;
            EventVariables = connectorEvent.EventParameters;
            ConnectorId = connectorEvent.ConnectorId;
            ConnectorVariables = connectorEvent.ConnectorVariables;
        }

        [JsonPropertyName("event_id")]
        public string EventId { get; set; }

        [JsonPropertyName("event_params")]
        public IEnumerable<ConnectorEventVariable> EventVariables { get; set; } = new List<ConnectorEventVariable>();

        [JsonPropertyName("connector_id")]
        public Guid? ConnectorId { get; set; } = null;

        [JsonPropertyName("connector_variables")]
        public IEnumerable<ConnectorVariable> ConnectorVariables { get; set; } = new List<ConnectorVariable>();

        public ConnectorEvent ToConnectorEvent(string userId, Connector connector)
        {
            var item = new ConnectorEvent()
            {
                UserId = userId,
                EventId = EventId,
                EventParameters = EventVariables,
                ConnectorVariables = ConnectorVariables,
                ConnectorId = connector.Id,
                ConnectorName = connector.Name,
                ConnectorColor = connector.Color
            };

            return item;
        }

    }
}