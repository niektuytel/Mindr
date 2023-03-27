﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    /// <summary>
    /// Used by hangfire to call connectors on time schedule 
    /// </summary>
    public class ConnectorEvent
    {
        public ConnectorEvent()
        {
            
        }

        public ConnectorEvent(ConnectorEvent @event, ConnectorBriefDTO connector)
        {
            Id = @event.Id;
            UserId = @event.UserId;
            EventId = @event.EventId;
            EventParams = @event.EventParams;
            ConnectorId = connector.Id;
            Variables = connector.Variables;
        }

        public ConnectorEvent(string userId, string eventId, Connector connector)
        {
            UserId = userId;
            EventId = eventId;
            ConnectorId = connector.Id;
            Variables = connector.Variables;
        }

        public ConnectorEvent(string eventId, IEnumerable<EventParam> eventParams)
        {
            EventId = eventId;
            EventParams = eventParams;
        }

        public ConnectorEvent(string eventId, IEnumerable<EventParam> eventParams, ConnectorBriefDTO connector)
        {
            EventId = eventId;
            EventParams = eventParams;
            ConnectorId = connector.Id;
            Variables = connector.Variables;
        }

        [Key]
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("userid")]
        public string? UserId { get; set; } = "";

        [JsonProperty("job_id")]
        public string JobId { get; set; } = "";

        [JsonProperty("event_id")]
        public string EventId { get; set; }

        [JsonProperty("event_params")]
        public IEnumerable<EventParam> EventParams { get; set; }

        [JsonProperty("connector_id")]
        public Guid? ConnectorId { get; set; } = null;

        [JsonProperty("connector_params")]
        public IEnumerable<ConnectorVariable> Variables { get; set; }

        public void Update(ConnectorEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
