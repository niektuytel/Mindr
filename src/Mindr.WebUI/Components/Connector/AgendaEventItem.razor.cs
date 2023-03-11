using DutchGrit.Afas;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models;
using Mindr.Core.Models.Connector;
using Newtonsoft.Json;

namespace Mindr.WebUI.Components.Connector;

public partial class AgendaEventItem: FluentComponentBase
{
    [Parameter, EditorRequired]
    public AgendaEvent Data { get; set; } = default!;

    [Parameter, EditorRequired]
    public ConnectorHookDialog HookDialogRef { get; set; } = default!;

    private ConnectorBriefDTO? SelectedConnector { get; set; } = null;

    private IEnumerable<ConnectorBriefDTO>? Connectors { get; set; } = null;

    private bool IsLoading { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7155/api/connector?eventId={Data.Id}");
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        var json = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrEmpty(json))
        {
            Connectors = JsonConvert.DeserializeObject<IEnumerable<ConnectorBriefDTO>>(json);
        }

        IsLoading = false;
    }

    
}
