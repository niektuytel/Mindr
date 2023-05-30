using Microsoft.AspNetCore.Components;
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
    public IDialogService DialogService { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    [Parameter, EditorRequired]
    public ConnectorOverviewDTO? Overview { get; set; } = null!;

    private bool DataHasChanged { get; set; } = false;

    private bool IsLoading { get; set; } = false;

    private bool success;

    public async Task HandleOnSave()
    {
        if (Overview == null) return;

        DataHasChanged = false;
        IsLoading = true;
        var response = await ConnectorClient.UpdateOverview(Overview);
        (_, var error) = response.AsTuple();
        IsLoading = false;

        if (!string.IsNullOrEmpty(error))
        {
            Snackbar.Add(error, Severity.Error);
        }
    }

    public async Task HandleOnDelete()
    {
        if (Overview == null) return;

        var parameters = new DialogParameters
        {
            { "Overview", Overview }
        };

        var dialog = await DialogService.ShowAsync<ConnectorDeleteDialog>("Delete", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            if (Overview == null) return;
            NavigationManager.NavigateTo($"/connectors");
        }
    }


}
