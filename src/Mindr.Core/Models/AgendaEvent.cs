using System;
using System.Text.Json.Serialization;

namespace Mindr.Core.Models
{
    public class AgendaEvent
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("start")]
        public AgendaEventDateTime StartDate { get; set; }

        [JsonPropertyName("end")]
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

//public class Value
//{
//    public string odataetag { get; set; }
//    public string id { get; set; }
//    public string subject { get; set; }
//    public Start start { get; set; }
//    public End end { get; set; }
//}

//public class Start
//{
//    public DateTime dateTime { get; set; }
//    public string timeZone { get; set; }
//}

//public class End
//{
//    public DateTime dateTime { get; set; }
//    public string timeZone { get; set; }
//}
