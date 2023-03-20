using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models;
using Newtonsoft.Json;
using Mindr.Core.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Mindr.WebUI.Services;

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

    [Inject]
    public IAccessTokenProvider TokenProvider { get; set; } = default!;

    public bool IsLoading { get; set; } = false;

    public string? Query { get; set; } = string.Empty;

    public IEnumerable<ConnectorBriefDTO> Results { get; set; } = new List<ConnectorBriefDTO>();

    public FluentDialog Dialog = default!;

    async Task HandleOnSearch(ChangeEventArgs args)
    {
        Results = new List<ConnectorBriefDTO>();

        if (args is not null && args.Value is not null)
        {
            string searchTerm = args.Value.ToString()!.ToLower();
            var response = await ConnectorClient.GetAll(query: searchTerm);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(json))
            {
                var value = JsonConvert.DeserializeObject<IEnumerable<ConnectorBriefDTO>>(json);
                if (value != null)
                {
                    Results = value;
                }
            }

            await Console.Out.WriteLineAsync();
            //var tokenResult = await TokenProvider.RequestAccessToken(new AccessTokenRequestOptions
            //{
            //    Scopes = new[] { "api://832f0468-7f76-4fb3-8d5c-7e5bd70d17ea/access_as_user" }
            //});

            //if (tokenResult.TryGetToken(out var accessToken))
            //{
            //    response.EnsureSuccessStatusCode();

            //    var json = await response.Content.ReadAsStringAsync();
            //    if (!string.IsNullOrEmpty(json))
            //    {
            //        var value = JsonConvert.DeserializeObject<IEnumerable<ConnectorBriefDTO>>(json);
            //        if (value != null)
            //        {
            //            Results = value;
            //        }
            //    }
            //}
        }

        IsLoading = false;
        base.StateHasChanged();
    }
    
    public async Task HandleOnUpsert()
    {
        IsLoading = true;

        var tokenResult = await TokenProvider.RequestAccessToken(new AccessTokenRequestOptions
        {
            Scopes = new[] { "api://832f0468-7f76-4fb3-8d5c-7e5bd70d17ea/access_as_user" }
        });
        if (tokenResult.TryGetToken(out var accessToken))
        {
            var hook = new ConnectorHook(CurrentHook, Data);
            await HookClient.Upsert(hook, accessToken.Value);
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


        var tokenResult = await TokenProvider.RequestAccessToken(new AccessTokenRequestOptions
        {
            Scopes = new[] { "api://832f0468-7f76-4fb3-8d5c-7e5bd70d17ea/access_as_user" }
        });
        if (tokenResult.TryGetToken(out var accessToken))
        {
            await HookClient.Delete(CurrentHook.Id, accessToken.Value);
        }

        Dialog.Hide();
        IsLoading = false;
        await OnChanged();
        base.StateHasChanged();
    }

    public void HandleOnDismiss(DialogEventArgs args)
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

    public async Task OpenDialog(AgendaEvent agendaEvent, ConnectorBriefDTO? connector = null)
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

}
