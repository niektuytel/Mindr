using Mindr.WebAssembly.Client.Interfaces;

namespace Mindr.WebAssembly.Client.Models.Options;

public class ApiOptions : IHasPosition
{
    public string Position => "Api";

    public string? BaseUrl { get; set; }

    public string[]? Scopes { get; set; }

}
