using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models;

namespace Mindr.WebUI.Pages.Agenda.Components;

public partial class AgendaEvents : FluentComponentBase
{
    [Parameter, EditorRequired]
    public IEnumerable<AgendaEvent> Data { get; set; } = default!;

    [Parameter, EditorRequired]
    public DateTime Date { get; set; } = default!;

    private ConnectorEventDialog _connectorEventDialog = default!;

    public async Task OnChanged()
    {
        StateHasChanged();
    }
}
