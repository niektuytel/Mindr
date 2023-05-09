using System.Text.Json.Serialization;
using System;

namespace Mindr.WebAssembly.Client.Models;

public class AgendaEventDateTime
{
    [JsonPropertyName("dateTime")]
    public DateTime DateTime { get; set; }

    [JsonPropertyName("timeZone")]
    public string TimeZone { get; set; }
}