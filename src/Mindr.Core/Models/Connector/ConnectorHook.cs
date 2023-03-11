using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    /// <summary>
    /// Used by hangfire to call connectors on time schedule 
    /// </summary>
    public class ConnectorHook
    {
        public ConnectorHook()
        {
            
        }
        public ConnectorHook(ConnectorHook hook, ConnectorBriefDTO connector)
        {
            Id = hook.Id;
            UserId = hook.UserId;
            EventId = hook.EventId;
            ConnectorId = connector.Id;
            Variables = connector.Variables;
        }

        public ConnectorHook(Guid userId, string eventId, Connector connector)
        {
            UserId = userId;
            EventId = eventId;
            ConnectorId = connector.Id;
            Variables = connector.Variables;
        }

        public ConnectorHook(Guid userId, string eventId, ConnectorBriefDTO connector)
        {
            UserId = userId;
            EventId = eventId;
            ConnectorId = connector.Id;
            Variables = connector.Variables;
        }

        public ConnectorHook(string eventId)
        {
            EventId = eventId;
        }

        public ConnectorHook(string eventId, ConnectorBriefDTO connector)
        {
            EventId = eventId;
            ConnectorId = connector.Id;
            Variables = connector.Variables;
        }

        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("userid")]
        public Guid? UserId { get; set; } = Guid.Empty;

        [JsonProperty("eventid")]
        public string EventId { get; set; }

        [JsonProperty("connectorid")]
        public Guid? ConnectorId { get; set; } = null;

        [JsonProperty("variables")]
        public IEnumerable<ConnectorVariable> Variables { get; set; }

        // TODO: What if not all inputs are provided for connector pipeline?
    }
}
