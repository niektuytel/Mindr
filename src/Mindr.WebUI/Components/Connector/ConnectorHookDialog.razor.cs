using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models;
using Newtonsoft.Json;
using Mindr.Core.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Mindr.WebUI.Services.Connector;
using Azure;

namespace Mindr.WebUI.Components.Connector;

public partial class ConnectorHookDialog: FluentComponentBase
{
    [Parameter, EditorRequired]
    public Func<Task> OnChanged { get; set; } = default!;

    [Parameter]
    public ConnectorHook? CurrentHook { get; set; } = null;

    [Parameter]
    public ConnectorBriefDTO? Data { get; set; } = null;

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IConnectorHookClient HookClient { get; set; } = default!;

    [Inject]
    public IConnectorClient ConnectorClient { get; set; } = default!;

    public bool IsLoading { get; set; } = false;

    public string? Query { get; set; } = string.Empty;

    public IEnumerable<ConnectorBriefDTO>? Results { get; set; } = null;

    public FluentDialog Dialog = default!;

    public async Task HandleOnSearch(ChangeEventArgs args)
    {
        Results = new List<ConnectorBriefDTO>();

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
                Results = JsonConvert.DeserializeObject<IEnumerable<ConnectorBriefDTO>>(json);
            }
        }

        IsLoading = false;
        base.StateHasChanged();
    }
    
    public async Task HandleOnUpsert()
    {
        IsLoading = true;

        var hook = new ConnectorHook(CurrentHook, Data);
        var response = await HookClient.Upsert(hook);
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
        if (CurrentHook == null) return;
        IsLoading = true;

        var response = await HookClient.Delete(CurrentHook.Id);
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

    public void HandleOnDialogOpen(AgendaEvent agendaEvent, ConnectorBriefDTO? connector = null)
    {
        Data = connector;
        if (connector != null)
        {
            Query = connector.Name;
            CurrentHook = new ConnectorHook(agendaEvent.Id, connector);
        }
        else
        {
            CurrentHook = new ConnectorHook(agendaEvent.Id);
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

        NavigationManager.NavigateTo($"/connector/{Data!.Id}");
        base.StateHasChanged();
    }


}
