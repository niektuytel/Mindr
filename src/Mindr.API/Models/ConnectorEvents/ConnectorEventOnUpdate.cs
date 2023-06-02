

using Mindr.Domain.Models.DTO.Connector;
using System.Text.Json.Serialization;

namespace Mindr.Api.Models.ConnectorEvents
{
    public class ConnectorEventOnUpdate
    {
        public ConnectorEventOnUpdate()
        { }

        public ConnectorEventOnUpdate(ConnectorEvent connectorEvent)
        {
            ConnectorId = connectorEvent.ConnectorId;
            ConnectorVariables = connectorEvent.ConnectorVariables;            
        }

        [JsonPropertyName("connector_id")]
        public Guid? ConnectorId { get; set; } = null;

        [JsonPropertyName("connector_variables")]
        public IEnumerable<ConnectorVariable> ConnectorVariables { get; set; } = new List<ConnectorVariable>();
    }
}