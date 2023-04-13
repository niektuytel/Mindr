using Mindr.Core.Models.Connector;
using Newtonsoft.Json;

namespace Mindr.Api.Models
{
    public class ConnectorEventOnUpdate
    {
        [JsonProperty("connector_variables")]
        public IEnumerable<ConnectorVariable> ConnectorVariables { get; set; } = new List<ConnectorVariable>();
    }
}