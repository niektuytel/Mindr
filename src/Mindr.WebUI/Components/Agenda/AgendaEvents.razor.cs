using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core;
using Mindr.Core.Models;
using Mindr.WebUI.Components.Connector;

namespace Mindr.WebUI.Components.Agenda;

public partial class AgendaEvents: FluentComponentBase
{
    [Parameter, EditorRequired]
    public IEnumerable<AgendaEvent> Data { get; set; } = default!;

    [Parameter, EditorRequired]
    public DateTime Date { get; set; } = default!;

    private ConnectorEventDialog _connectorEventDialog = default!;

    public async Task OnChanged()
    {
        base.StateHasChanged();
    }
}
