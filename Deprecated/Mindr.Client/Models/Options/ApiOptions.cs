using Mindr.Client.Interfaces;

namespace Mindr.Client.Models.Options
{
    public class ApiOptions : IHasPosition
    {
        public string Position => "Api";

        public string? BaseUrl { get; set; }

        public string[]? Scopes { get; set; }

    }
}
