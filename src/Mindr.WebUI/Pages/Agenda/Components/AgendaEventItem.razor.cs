using DutchGrit.Afas;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models;
using Mindr.Core.Models.Connector;
using Mindr.WebUI.Components;
using Mindr.WebUI.Services;
using Newtonsoft.Json;

namespace Mindr.WebUI.Pages.Agenda.Components;

public partial class AgendaEventItem : FluentComponentBase
{
    [Parameter, EditorRequired]
    public AgendaEvent Data { get; set; } = default!;

    [Parameter, EditorRequired]
    public ConnectorEventDialog EventDialogRef { get; set; } = default!;

    [Inject]
    public IHttpConnectorClient ConnectorClient { get; set; } = default!;

    [Inject]
    public IHttpConnectorEventClient ConnectorEventClient { get; set; } = default!;

    private IEnumerable<ConnectorEvent>? Connectors { get; set; } = null;

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
            Connectors = JsonConvert.DeserializeObject<IEnumerable<ConnectorEvent>>(json);
        }

        IsLoading = false;
    }


}
