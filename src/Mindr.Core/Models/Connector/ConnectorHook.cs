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

        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("userid")]
        public Guid UserId { get; set; }

        [JsonProperty("userid")]
        public string EventId { get; set; }

        [JsonProperty("connectorid")]
        public Guid ConnectorId { get; set; }

        [JsonProperty("variables")]
        public IEnumerable<ConnectorVariable> Variables { get; set; }

        // TODO: What if not all inputs are provided for connector pipeline?
    }
}
