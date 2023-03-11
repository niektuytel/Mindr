using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Interfaces;
using Mindr.Core;
using Mindr.Core.Models;

namespace Mindr.WebUI.Components.Connector;

public partial class AgendaEvents: FluentComponentBase
{
    [Parameter, EditorRequired]
    public IEnumerable<AgendaEvent> Data { get; set; } = default!;

    [Parameter, EditorRequired]
    public DateTime DateTime { get; set; } = default!;

    private ConnectorHookDialog _connectorHookDialog;

    protected override async Task OnInitializedAsync()
    {
    }
}
