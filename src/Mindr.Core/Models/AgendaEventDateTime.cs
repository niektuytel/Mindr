using Newtonsoft.Json;
using System;

namespace Mindr.Core.Models
{
    public class AgendaEventDateTime
    {
        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }
    }
}