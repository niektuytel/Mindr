using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;

namespace Mindr.Client.Pages
{
    public partial class ConnectorEventsPage : FluentComponentBase
    {
        [Parameter]
        public string? EventId { get; set; }

    }
}
