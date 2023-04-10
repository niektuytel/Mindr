
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models;
using Newtonsoft.Json;
using Mindr.Core.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Azure;
using Mindr.Core.Enums;
using Mindr.WebUI.Services.ApiClients;

namespace Mindr.WebUI.Components;

public partial class ConnectorEventDialog: FluentComponentBase
{
    [Parameter, EditorRequired]
    public Func<Task> OnChanged { get; set; } = default!;

    [Parameter]
    public ConnectorEvent? CurrentEvent { get; set; } = null;

    [Parameter]
    public Connector? Data { get; set; } = null;

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IHttpConnectorEventClient EventClient { get; set; } = default!;

    [Inject]
    public IHttpConnectorClient ConnectorClient { get; set; } = default!;

    public bool IsLoading { get; set; } = false;

    public string? Query { get; set; } = string.Empty;

    public IEnumerable<Connector>? Results { get; set; } = null;

    public FluentDialog Dialog = default!;

    public async Task HandleOnSearch(ChangeEventArgs args)
    {
        Results = new List<Connector>();

        if (args is not null && args.Value is not null)
        {
            string searchTerm = args.Value.ToString()!.ToLower();
            var response = await ConnectorClient.GetAll(query: searchTerm);
            if (response == null)
            {
                // Failed request
                throw new NotImplementedException();
            }

            var json = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(json))
            {
                Results = JsonConvert.DeserializeObject<IEnumerable<Connector>>(json);
            }
        }

        IsLoading = false;
        base.StateHasChanged();
    }

    public async Task HandleOnSelect(EventArgs args)
    {
        await Console.Out.WriteLineAsync(   );


        base.StateHasChanged();
    }

    public async Task HandleOnUpsert()
    {
        IsLoading = true;

        var @event = new ConnectorEvent(CurrentEvent, Data);
        var response = await EventClient.Upsert(@event);
        if (response == null) 
        {
            // Failed request
            throw new NotImplementedException();
        }

        Dialog.Hide();
        IsLoading = false;
        await OnChanged();
        base.StateHasChanged();
    }

    public async Task HandleOnDelete()
    {
        if (CurrentEvent == null) return;
        IsLoading = true;

        var response = await EventClient.Delete(CurrentEvent.Id);
        if (response == null)
        {
            // Failed request
            throw new NotImplementedException();
        }

        Dialog.Hide();
        IsLoading = false;
        await OnChanged();
        base.StateHasChanged();
    }

    public void HandleOnDialogOpen(AgendaEvent agendaEvent, Connector? connector = null)
    {
        Data = connector;
        var events = new List<EventParam>
        {
            new EventParam()
            {
                Type = EventType.OnDateTime,
                Value = agendaEvent.StartDate.DateTime.ToLongDateString()
            }
        };

        if (connector != null)
        {
            Query = connector.Name;

            CurrentEvent = new ConnectorEvent(agendaEvent.Id, events, connector);
        }
        else
        {
            CurrentEvent = new ConnectorEvent(agendaEvent.Id, events);
        }

        Dialog.Show();
        base.StateHasChanged();
    }
    
    public void HandleOnDialogDismiss(DialogEventArgs args)
    {
        if (args is not null && args.Reason is not null && args.Reason == "dismiss")
        {
            Dialog.Hide();
        }
    }

    public void GoToConnector()
    {
        if (Data == null) return;

        NavigationManager.NavigateTo($"/connectors/{Data!.Id}");
        base.StateHasChanged();
    }


}
