using Mindr.Domain.HttpRunner.Models;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mindr.Domain.Models.DTO.Connector
{
    /// <summary>
    /// Used to execute pipeline with etc.
    /// </summary>
    public class Connector
    {
        public Connector() { }

        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; } = GetRandomColorClass();

        [JsonPropertyName("total_using")]
        public int TotalUsing { get; set; } = 0;

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("variables")]
        public IEnumerable<ConnectorVariable> Variables { get; set; } = new List<ConnectorVariable>();

        [JsonPropertyName("pipeline")]
        public IEnumerable<HttpItem> Pipeline { get; set; } = new List<HttpItem>();

        private static string GetRandomColorClass()
        {
            string[] colors = new[] { "magenta", "yellow", "green", "pink", "red" };
            var random = new Random();
            return colors[random.Next(0, colors.Length)];
        }

    }
}
