using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;

using Mindr.WebAssembly.Client.Pages.Connectors.Components;
using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.Models.DTO.Connector;
using System.Threading.Tasks;
using MudBlazor;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Views;

public partial class ConnectorOverview
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    [Parameter, EditorRequired]
    public ConnectorOverviewDTO? Overview { get; set; } = null!;

    private bool DataHasChanged { get; set; } = false;

    public ConfirmDialog RemoveItemDialog = default!;

    public async Task OnSave()
    {
        if (Overview == null) return;

        var response = await ConnectorClient.UpdateOverview(Overview);
        (_, var error) = response.AsTuple();

        if (!string.IsNullOrEmpty(error))
        {
            Snackbar.Add(error, Severity.Error);
            base.StateHasChanged();
        }
    }

    public async Task OnRemove()
    {
        if (Overview == null) return;

        var response = await ConnectorClient.Delete(Overview.Id.ToString());
        (_, var error) = response.AsTuple();
        if (!string.IsNullOrEmpty(error))
        {
            Snackbar.Add(error, Severity.Error);
            base.StateHasChanged();
            return;
        }

        NavigationManager.NavigateTo($"/connectors");
        base.StateHasChanged();
    }

    private void OnChangeName(string value)
    {
        if (Overview == null) return;

        Overview.Name = value;
        DataHasChanged = true;
        base.StateHasChanged();
    }

    private void OnChangeDescription(string value)
    {
        if (Overview == null) return;

        Overview.Description = value;
        DataHasChanged = true;
        base.StateHasChanged();
    }

}
