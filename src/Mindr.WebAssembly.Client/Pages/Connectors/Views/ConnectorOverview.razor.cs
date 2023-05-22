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
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; }

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    [Parameter, EditorRequired]
    public ConnectorOverviewDTO Overview { get; set; } = null!;

    private bool DataHasChanged = false;

    public ConfirmDialog RemoveItemDialog = default!;

    public async Task OnSave()
    {
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
        var response = await ConnectorClient.Delete(Overview.Id.ToString());
        (_, ErrorMessage) = response.AsTuple();
        if (!string.IsNullOrEmpty(ErrorMessage))
        {
            base.StateHasChanged();
            return;
        }

        NavigationManager.NavigateTo($"/connectors");
        base.StateHasChanged();
    }

    private void OnChangeName(string value)
    {
        Overview.Name = value;
        OnDataChanged();
    }

    private void OnChangeDescription(string value)
    {
        Overview.Description = value;
        OnDataChanged();
    }

    private void OnDataChanged()
    {
        DataHasChanged = true;
        base.StateHasChanged();
    }

}
