using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Mindr.Api.Models.Connectors
{
    public class ConnectorOnCreate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}