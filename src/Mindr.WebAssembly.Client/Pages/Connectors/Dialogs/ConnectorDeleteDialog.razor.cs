using Microsoft.AspNetCore.Components;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.WebAssembly.Client.Services;
using MudBlazor;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Dialogs;

public partial class ConnectorDeleteDialog
{
    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    [CascadingParameter]
    public MudDialogInstance Dialog { get; set; } = default!;

    [Parameter, EditorRequired]
    public ConnectorOverviewDTO Overview { get; set; } = default!;

    private bool IsLoading = false;

    public async Task HandleOnConfirm()
    {
        IsLoading = true;
        var response = await ConnectorClient.Delete(Overview.Id.ToString());
        (_, var error) = response.AsTuple();
        IsLoading = false;

        if (!string.IsNullOrEmpty(error))
        {
            Snackbar.Add(error, Severity.Error);
            return;
        }

        Dialog.Close();
    }

    public void HandleDialogClose()
    {
        Dialog.Cancel();
    }

}
