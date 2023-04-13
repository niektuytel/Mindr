using Mindr.Core.Models.Connector.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Mindr.Core.Models.Connector
{
    /// <summary>
    /// Used by hangfire to call connectors on time schedule 
    /// </summary>
    public class ConnectorEventBrieflyDTO
    {

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
        public IEnumerable<EventParameter> EventParams { get; set; }

        [JsonProperty("connector_id")]
        public Guid? ConnectorId { get; set; } = null;

        [JsonProperty("connector_name")]
        public string ConnectorName { get; set; } = "";

        [JsonProperty("connector_params")]
        public IEnumerable<ConnectorVariable> Variables { get; set; } = new List<ConnectorVariable>();

        [JsonProperty("color")]
        public string Color { get; set; } = "#000000";
    }
}
