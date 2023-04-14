
using Mindr.Core.Models.Connectors;
using Newtonsoft.Json;

namespace Mindr.Api.Models.ConnectorEvents
{
    public class ConnectorEventOnUpdate
    {
        [JsonProperty("connector_variables")]
        public IEnumerable<ConnectorVariable> ConnectorVariables { get; set; } = new List<ConnectorVariable>();
    }
}