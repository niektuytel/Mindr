using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Shared.Models;
using Mindr.Shared.Models.ConnectorEvents;
using Mindr.Client.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Mindr.Client.Pages.Agenda.Components;

public partial class AgendaEventItem : FluentComponentBase
{
    [Parameter, EditorRequired]
    public AgendaEvent Data { get; set; } = default!;

    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; } = default!;

    [Inject]
    public IApiConnectorEventClient ConnectorEventClient { get; set; } = default!;

    private IEnumerable<ConnectorEvent>? Connectors { get; set; } = null;


    private ConnectorEventDialog _connectorEventDialog = default!;

    private bool IsLoading { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;

        var response = await ConnectorEventClient.GetAll(eventId: Data.Id);
        (Connectors, var error) = response.AsTuple();
        if (!string.IsNullOrEmpty(error))
        {
            base.StateHasChanged();
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
