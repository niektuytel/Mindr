﻿using Mindr.Client.Interfaces;

namespace Mindr.Client.Models.Options
{
    public class MicrosoftGraphOptions : IHasPosition
    {
        public string Position => "MicrosoftGraph";

        public string? BaseUrl { get; set; }
        public string? Version { get; set; }
        public string[]? Scopes { get; set; }

    }
}
