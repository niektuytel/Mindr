using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;

namespace Mindr.WebAssembly.Client.Pages.ConnectorEvents;

public partial class ConnectorEventsPage : FluentComponentBase
{
    [Parameter]
    public string? EventId { get; set; }

}
