using Newtonsoft.Json;

namespace Mindr.WebUI.Models;

public class HttpOverview
{
    [JsonProperty("name"), JsonRequired]
    public string? Name { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("item")]
    public HttpOverview[]? Overview { get; set; } = null;

    [JsonProperty("request")]
    public HttpRequest Request { get; set; } = null;

    [JsonProperty("response")]
    public HttpResponse[] Response { get; set; } = null;
}
