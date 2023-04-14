using Mindr.WebUI.Interfaces;

namespace Mindr.WebUI.Models.Options
{
    public class ApiOptions : IHasPosition
    {
        public string Position => "Api";

        public string? BaseUrl { get; set; }

        public string[]? Scopes { get; set; }

    }
}
