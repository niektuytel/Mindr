using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models;
using Mindr.Core.Models.ConnectorEvents;
using Mindr.WebUI.Services;
using Newtonsoft.Json;

namespace Mindr.WebUI.Pages.Agenda.Components;

public partial class AgendaEventItem : FluentComponentBase
{
    [Parameter, EditorRequired]
    public AgendaEvent Data { get; set; } = default!;

    [Inject]
    public IHttpConnectorClient ConnectorClient { get; set; } = default!;

    [Inject]
    public IHttpConnectorEventClient ConnectorEventClient { get; set; } = default!;

    private IEnumerable<ConnectorEvent>? Connectors { get; set; } = null;


    private ConnectorEventDialog _connectorEventDialog = default!;

    private bool IsLoading { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;

        var response = await ConnectorEventClient.GetAll(eventId: Data.Id);
        if (response == null)
        {
            // Failed request
            throw new NotImplementedException();
        }

        var json = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrEmpty(json))
        {
            Connectors = JsonConvert.DeserializeObject<IEnumerable<ConnectorEvent>>(json);
        }

        IsLoading = false;
    }


    public async Task OnUpdate(ConnectorEvent connectorEvent)
    {
        if (Connectors == null) return;

        // update
        Connectors = Connectors.Select(x => x.Id == connectorEvent.Id ? connectorEvent : x);

        StateHasChanged();
    }

    public async Task OnCreate(ConnectorEvent connectorEvent)
    {
        if (Connectors == null) return;

        // insert
        var connectors = Connectors.ToList();
        connectors.Add(connectorEvent);
        Connectors = connectors.ToArray();

        StateHasChanged();
    }

    public async Task OnDelete(ConnectorEvent connectorEvent)
    {
        if (Connectors == null) return;

        // delete
        Connectors = Connectors.Where(x => x.Id != connectorEvent.Id);

        StateHasChanged();
    }

}
