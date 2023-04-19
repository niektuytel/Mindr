﻿
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Enums;
using Mindr.Core.Models;
using Mindr.Core.Models.ConnectorEvents;
using Mindr.Core.Models.Connectors;
using Mindr.WebUI.Services;
using Newtonsoft.Json;

namespace Mindr.WebUI.Pages.Agenda.Components;

public partial class ConnectorEventDialog : FluentComponentBase
{
    [Parameter, EditorRequired]
    public Func<ConnectorEvent, Task> OnUpdate { get; set; } = default!;

    [Parameter, EditorRequired]
    public Func<ConnectorEvent, Task> OnCreate { get; set; } = default!;

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

    private string? ErrorMessage { get; set; }

    public bool IsLoading { get; set; } = false;

    public string? Query { get; set; } = string.Empty;

    public bool IsCreating { get; set; } = true;

    public IEnumerable<Connector>? Results { get; set; } = null;

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

    public async Task HandleOnSelect(Connector? input)
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

        base.StateHasChanged();
    }

    public async Task HandleOnUpsert()
    {
        if (IsLoading || ConnectorEvent == null || AgendaEvent == null) return;

        IsLoading = true;

        if (IsCreating)
        {
            var events = new List<ConnectorEventParameter>
            {
                new ConnectorEventParameter()
                {
                    Key = EventType.OnDateTime,
                    Value = AgendaEvent.StartDate.DateTime.ToLongDateString()
                }
            };

            ConnectorEvent.Id = Guid.NewGuid();
            ConnectorEvent.EventId = AgendaEvent.Id;
            ConnectorEvent.EventParameters = events;

            var response = await ConnectorEventClient.Create(ConnectorEvent);
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
                    ConnectorEvent = JsonConvert.DeserializeObject<ConnectorEvent>(content);
                    await OnCreate(ConnectorEvent!);
                    Dialog.Hide();
                }
                else
                {
                    ErrorMessage = content;
                    base.StateHasChanged();
                }
            }
        }
        else
        {
            var response = await ConnectorEventClient.Update(ConnectorEvent.Id, ConnectorEvent);
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
                    ConnectorEvent = JsonConvert.DeserializeObject<ConnectorEvent>(content);
                    await OnUpdate(ConnectorEvent!);
                    Dialog.Hide();
                }
                else
                {
                    ErrorMessage = content;
                    base.StateHasChanged();
                }
            }
        }

        IsLoading = false;
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
