using Microsoft.AspNetCore.Components;
using Mindr.WebAssembly.Client.Pages.Connectors.Components;
using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.Models.DTO.Connector;
using MudBlazor;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Views;

public partial class ConnectorsView
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; } = default!;

    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    private bool IsLoading = false;
    private IEnumerable<ConnectorBriefDTO> DataCollection = new List<ConnectorBriefDTO>();

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        var response = await ConnectorClient.GetAll();
        (DataCollection, var error) = response.AsTuple();
        IsLoading = false;
        
        if (!string.IsNullOrEmpty(error))
        {
            Snackbar.Add(error, Severity.Error);
            base.StateHasChanged();
        }

    }

    public async Task HandleDialogOpen()
    {
        var dialog = await DialogService.ShowAsync<ConnectorDialog>();
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            if(Guid.TryParse(result.Data.ToString(), out Guid connectorId))
            {
                NavigationManager.NavigateTo($"/connectors/{connectorId}/pipeline");
            }
            else
            {
                Snackbar.Add($"Can't navigate, connector id is invalid", Severity.Error);
            }
        }
    }

    public void HandleRowClick(TableRowClickEventArgs<ConnectorBriefDTO> args)
    {
        var item = args.Item;
        if (item == null) return;

        NavigationManager.NavigateTo($"/connectors/{item.Id}/overview");
    }

}
