using DutchGrit.Afas;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models;
using Mindr.Core.Models.Connector;
using Mindr.WebUI.Services;
using Newtonsoft.Json;

namespace Mindr.WebUI.Components.Connector;

public partial class AgendaEventItem: FluentComponentBase
{
    [Parameter, EditorRequired]
    public AgendaEvent Data { get; set; } = default!;

    [Parameter, EditorRequired]
    public ConnectorHookDialog HookDialogRef { get; set; } = default!;

    [Inject]
    public IAccessTokenProvider TokenProvider { get; set; } = default!;

    [Inject]
    public IConnectorClient ConnectorClient { get; set; } = default!;

    private ConnectorBriefDTO? SelectedConnector { get; set; } = null;

    private IEnumerable<ConnectorBriefDTO>? Connectors { get; set; } = null;

    private bool IsLoading { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;

        var response = await ConnectorClient.GetAll(eventId:Data.Id);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrEmpty(json))
        {
            Connectors = JsonConvert.DeserializeObject<IEnumerable<ConnectorBriefDTO>>(json);
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
        //        Connectors = JsonConvert.DeserializeObject<IEnumerable<ConnectorBriefDTO>>(json);
        //    }
        //}

        IsLoading = false;
    }

    
}
