using Mindr.Core.Models.Connector.Http;
using Mindr.Core.Models.Connector;
using Newtonsoft.Json;

namespace Mindr.Api.Models
{
    public class ConnectorInsertResponse
    {

        public ConnectorInsertResponse()
        {

        }

        public ConnectorInsertResponse(Connector connector)
        {
            CreatedBy = connector.CreatedBy;
            Color = connector.Color;
            Name = connector.Name;
            Description = connector.Description;
        }

        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; } = "Unique Creator identifier";

        [JsonProperty("color")]
        public string Color { get; set; } = GetRandomColorClass();

        [JsonProperty("name")]
        public string Name { get; set; } = "Name of test connector";

        [JsonProperty("description")]
        public string Description { get; set; } = "Description about this test";

        private static string GetRandomColorClass()
        {
            string[] colors = new[] { "magenta", "yellow", "green", "pink", "red" };
            var random = new Random();
            return colors[random.Next(0, colors.Length)];
        }

    }
}