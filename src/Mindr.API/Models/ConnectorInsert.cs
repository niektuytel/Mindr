using Mindr.Core.Models.Connector.Http;
using Mindr.Core.Models.Connector;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Mindr.Api.Models
{
    public class ConnectorInsert
    {

        public ConnectorInsert()
        {

        }

        [JsonIgnore]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonIgnore]
        public string CreatedBy { get; set; } = "Unique Creator identifier";

        [JsonIgnore]
        public string Color { get; set; } = GetRandomColorClass();

        private static string GetRandomColorClass()
        {
            string[] colors = new[] { "magenta", "yellow", "green", "pink", "red" };
            var random = new Random();
            return colors[random.Next(0, colors.Length)];
        }

        public Connector MapToConnector()
        {
            return new Connector()
            {
                Id = Id,
                CreatedBy = CreatedBy,
                Color = Color,
                Name = Name,
                Description = Description
            };
        }
    }
}