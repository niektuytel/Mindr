using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Mindr.Shared.Models
{
    public class AgendaEvents
    {
        [JsonPropertyName("odatacontext")]
        public string ODataContext { get; set; }

        [JsonPropertyName("value")]
        public IEnumerable<AgendaEvent> Events { get; set; }

    }
}

