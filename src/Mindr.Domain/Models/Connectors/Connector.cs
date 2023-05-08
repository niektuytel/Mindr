using Mindr.HttpRunner.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mindr.Shared.Models.Connectors
{
    /// <summary>
    /// Used to execute pipeline with etc.
    /// </summary>
    public class Connector
    {
        public Connector() { }

        [Key]
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; } = GetRandomColorClass();

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; set; }

        [JsonProperty("variables")]
        public IEnumerable<ConnectorVariable> Variables { get; set; } = new List<ConnectorVariable>();

        [JsonProperty("pipeline")]
        public IEnumerable<HttpItem> Pipeline { get; set; } = new List<HttpItem>();

        private static string GetRandomColorClass()
        {
            string[] colors = new[] { "magenta", "yellow", "green", "pink", "red" };
            var random = new Random();
            return colors[random.Next(0, colors.Length)];
        }

    }
}
