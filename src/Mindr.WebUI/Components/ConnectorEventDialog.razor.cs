
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
using Mindr.WebUI.Components.Agenda;

namespace Mindr.WebUI.Components;

public partial class ConnectorEventDialog: FluentComponentBase
{
    [Parameter, EditorRequired]
    public Func<Task> OnChanged { get; set; } = default!;

    [Parameter]
    public ConnectorEvent? ConnectorEvent { get; set; } = null;

    public AgendaEvent? AgendaEvent { get; set; } = null;

    //[Parameter]
    //public Connector? Data { get; set; } = null;

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IHttpConnectorEventClient ConnectorEventClient { get; set; } = default!;

    [Inject]
    public IHttpConnectorClient ConnectorClient { get; set; } = default!;

    public bool IsLoading { get; set; } = false;

    public string? Query { get; set; } = string.Empty;

    public bool IsCreating { get; set; } = true;

    public IEnumerable<ConnectorEvent>? Results { get; set; } = null;

    public FluentDialog Dialog = default!;

    public async Task HandleOnSearch(ChangeEventArgs args)
    {
        Results = new List<ConnectorEvent>();

        if (args is not null && args.Value is not null)
        {
            string searchTerm = args.Value.ToString()!.ToLower();
            var response = await ConnectorEventClient.GetAll(query: searchTerm);
            if (response == null)
            {
                // Failed request
                throw new NotImplementedException();
            }

            var json = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(json))
            {
                Results = JsonConvert.DeserializeObject<IEnumerable<ConnectorEvent>>(json);
            }
        }

        IsLoading = false;
        base.StateHasChanged();
    }

    public async Task HandleOnSelect(ConnectorEvent? input)
    {
        if (input == null) return;

        ConnectorEvent = input;
        base.StateHasChanged();
    }

    public async Task HandleOnUpsert()
    {
        if (IsLoading || ConnectorEvent == null || AgendaEvent == null) return;

        IsLoading = true;

        if(IsCreating)
        {
            var events = new List<EventParam>
            {
                new EventParam()
                {
                    Type = EventType.OnDateTime,
                    Value = AgendaEvent.StartDate.DateTime.ToLongDateString()
                }
            };
            ConnectorEvent.EventId = AgendaEvent.Id;
            ConnectorEvent.EventParams = events;

            var response = await ConnectorEventClient.Create(ConnectorEvent);
            if (response == null) 
            {
                // Failed request
                throw new NotImplementedException();
            }
        }
        else
        {
            var response = await ConnectorEventClient.Update(ConnectorEvent.Id, ConnectorEvent);
            if (response == null)
            {
                // Failed request
                throw new NotImplementedException();
            }
        }

        Dialog.Hide();
        IsLoading = false;
        await OnChanged();
        base.StateHasChanged();
    }

    public async Task HandleOnDelete()
    {
        if (ConnectorEvent == null) return;
        IsLoading = true;

        var response = await ConnectorEventClient.Delete(ConnectorEvent.Id);
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

    public void HandleOnDialogOpen(AgendaEvent agendaEvent, ConnectorEvent? connectorEvent = null)
    {
        AgendaEvent = agendaEvent;
        IsCreating = connectorEvent == null;

        if (connectorEvent != null)
        {
            Query = connectorEvent.ConnectorName;
            ConnectorEvent = connectorEvent;

        }
        else
        {
            ConnectorEvent = new ConnectorEvent();
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
        if (ConnectorEvent == null) return;

        NavigationManager.NavigateTo($"/connectors/{ConnectorEvent!.ConnectorId}");
        base.StateHasChanged();
    }


}
