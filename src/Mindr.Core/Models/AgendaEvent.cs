using Newtonsoft.Json;
using System;

namespace Mindr.Core.Models
{
    public class AgendaEvent
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("start")]
        public AgendaEventDateTime StartDate { get; set; }

        [JsonProperty("end")]
        public AgendaEventDateTime EndDate { get; set; }

        public string Color { get; set; } = GetRandomColorClass();
        private static string GetRandomColorClass()
        {
            string[] colors = new[] { "magenta", "yellow", "green", "pink", "red" };
            var random = new Random();
            return colors[random.Next(0, colors.Length)];
        }
    }
}