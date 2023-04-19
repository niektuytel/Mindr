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

    private IEnumerable<ConnectorEvent>? ConnectorEvents { get; set; } = null;

    private string? ErrorMessage { get; set; }

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
            ConnectorEvents = JsonConvert.DeserializeObject<IEnumerable<ConnectorEvent>>(json);
        }

        IsLoading = false;
    }


    public async Task OnUpdate(ConnectorEvent connectorEvent)
    {
        if (ConnectorEvents == null) return;

        // update
        ConnectorEvents = ConnectorEvents.Select(x => x.Id == connectorEvent.Id ? connectorEvent : x);

        StateHasChanged();
    }

    public async Task OnCreate(ConnectorEvent connectorEvent)
    {
        if (ConnectorEvents == null) return;

        // insert
        var connectors = ConnectorEvents.ToList();
        connectors.Add(connectorEvent);
        ConnectorEvents = connectors.ToArray();

        StateHasChanged();
    }

    public async Task OnDelete(ConnectorEvent connectorEvent)
    {
        if (connectorEvent == null || ConnectorEvents == null) return;
        IsLoading = true;

        var response = await ConnectorEventClient.Delete(connectorEvent.Id);
        if (response == null)
        {
            // TODO: should be fixed with refresh token

            ErrorMessage = $"Login session expired, Please login again";
            base.StateHasChanged();
        }
        else
        {
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                // delete
                ConnectorEvents = ConnectorEvents.Where(x => x.Id != connectorEvent.Id);
            }
            else
            {
                ErrorMessage = content;
                base.StateHasChanged();
            }
        }

        IsLoading = false;
        base.StateHasChanged();
    }


}
