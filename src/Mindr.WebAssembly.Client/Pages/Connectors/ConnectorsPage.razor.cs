using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;

namespace Mindr.WebAssembly.Client.Pages.Connectors
{
    public partial class ConnectorsPage : FluentComponentBase
    {
        [Parameter]
        public string? ConnectorId { get; set; }

        [Parameter]
        public string? NavName { get; set; }
    }
}
