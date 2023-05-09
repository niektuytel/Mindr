
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Domain.Enums;
using Mindr.Domain.Models;
using Mindr.Domain.Models.DTO.Connector;

using Mindr.WebAssembly.Client.Services;
using System.Text.Json.Serialization;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Pages.Agenda.Components;

public partial class ConnectorEventDialog : FluentComponentBase
{
    [Parameter, EditorRequired]
    public Func<ConnectorEvent, Task> OnUpdate { get; set; } = default!;

    [Parameter, EditorRequired]
    public Func<ConnectorEvent, Task> OnCreate { get; set; } = default!;

    [Parameter, EditorRequired]
    public Func<ConnectorEvent, Task> OnDelete { get; set; } = default!;

    [Parameter]
    public ConnectorEvent? ConnectorEvent { get; set; } = null;

    public AgendaEvent? AgendaEvent { get; set; } = null;

    //[Parameter]
    //public Connector? Data { get; set; } = null;

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IApiConnectorEventClient ConnectorEventClient { get; set; } = default!;

    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; } = default!;

    private string? ErrorMessage { get; set; }

    public bool IsLoading { get; set; } = false;

    public string? Query { get; set; } = string.Empty;

    public bool IsCreating { get; set; } = true;

    public IEnumerable<ConnectorBriefDTO>? Results { get; set; } = null;

    public FluentDialog Dialog = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Dialog.Hide();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    public async Task HandleOnSearch(ChangeEventArgs args)
    {
        Results = new List<ConnectorBriefDTO>();

        if (args is not null && args.Value is not null)
        {
            string searchTerm = args.Value.ToString()!.ToLower();

            var response = await ConnectorClient.GetAll(query: searchTerm);
            (Results, ErrorMessage) = response.AsTuple();
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                base.StateHasChanged();
            }
        }

        IsLoading = false;
        Query = args?.Value?.ToString() ?? "";
        base.StateHasChanged();
    }

    public async Task HandleOnSelect(ConnectorBriefDTO? input)
    {
        if (input == null) return;

        var variables = input.Variables
            .Where(item => item.IsPublic)
            .Select(item => 
                new ConnectorVariable()
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    Description = item.Description, 
                    Key = item.Key,
                    Value = item.Value
                }
            );

        ConnectorEvent = new ConnectorEvent()
        {
            Id = ConnectorEvent?.Id ?? Guid.NewGuid(),
            ConnectorId = input.Id,
            ConnectorName = input.Name,
            ConnectorVariables = variables,
            ConnectorColor = input.Color,
        };


        Query = ConnectorEvent.ConnectorName;
        base.StateHasChanged();
    }

    public async Task HandleOnUpsert()
    {
        if (IsLoading || ConnectorEvent == null || AgendaEvent == null) return;

        IsLoading = true;

        if (IsCreating)
        {
            var events = new List<ConnectorEventVariable>
            {
                new ConnectorEventVariable()
                {
                    Key = EventType.OnDateTime,
                    Value = AgendaEvent.StartDate.DateTime.ToLongDateString()
                }
            };

            ConnectorEvent.Id = Guid.NewGuid();
            ConnectorEvent.EventId = AgendaEvent.Id;
            ConnectorEvent.EventParameters = events;

            var response = await ConnectorEventClient.Create(ConnectorEvent);
            (ConnectorEvent, ErrorMessage) = response.AsTuple();
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                base.StateHasChanged();
            }
            else if (ConnectorEvent != null)
            {
                await OnCreate(ConnectorEvent!);
                Dialog.Hide();
            }
        }
        else
        {
            var response = await ConnectorEventClient.Update(ConnectorEvent.Id, ConnectorEvent);
            (ConnectorEvent, ErrorMessage) = response.AsTuple();
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                base.StateHasChanged();
            }
            else if(ConnectorEvent != null)
            {
                await OnUpdate(ConnectorEvent!);
                Dialog.Hide();
            }
        }

        IsLoading = false;
        Query = ConnectorEvent!.ConnectorName;
        base.StateHasChanged();
    }

    public async Task HandleOnDelete()
    {
        if (ConnectorEvent == null) return;
        IsLoading = true;

        var response = await ConnectorEventClient.Delete(ConnectorEvent.Id);
        (ConnectorEvent, ErrorMessage) = response.AsTuple();
        if (!string.IsNullOrEmpty(ErrorMessage))
        {
            base.StateHasChanged();
        }
        else if(ConnectorEvent != null)
        {
            await OnDelete(ConnectorEvent!);
            Dialog.Hide();
        }

        IsLoading = false;
        Query = "";
        base.StateHasChanged();
    }

    public void HandleOnEdit(AgendaEvent agendaEvent, ConnectorEvent connectorEvent)
    {
        AgendaEvent = agendaEvent;
        IsCreating = false;

        ConnectorEvent = connectorEvent;
        
        Query = ConnectorEvent.ConnectorName;
        Dialog.Show();
        base.StateHasChanged();
    }

    public void HandleOnCreate(AgendaEvent agendaEvent)
    {
        AgendaEvent = agendaEvent;
        IsCreating = true;

        ConnectorEvent = new ConnectorEvent();

        Dialog.Show();
        Query = ConnectorEvent.ConnectorName;
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
