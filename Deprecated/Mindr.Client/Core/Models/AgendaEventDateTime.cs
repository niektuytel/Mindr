using Newtonsoft.Json;
using System;

namespace Mindr.Shared.Models
{
    public class AgendaEventDateTime
    {
        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }
    }
}