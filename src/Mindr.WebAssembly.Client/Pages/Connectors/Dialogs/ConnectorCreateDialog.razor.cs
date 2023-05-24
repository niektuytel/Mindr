using Microsoft.AspNetCore.Components;
using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.Models.DTO.Connector;
using MudBlazor;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Dialogs;

public partial class ConnectorCreateDialog
{
    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    [CascadingParameter]
    public MudDialogInstance Dialog { get; set; } = default!;

    [Parameter]
    public Connector Connector { get; set; } = new Connector();

    private bool IsLoading = false;
    
    public async Task HandleOnCreate()
    {
        IsLoading = true;
        var response = await ConnectorClient.Create(Connector);
        (var data, var error) = response.AsTuple();
        IsLoading = false;

        if (!string.IsNullOrEmpty(error))
        {
            Snackbar.Add(error, Severity.Error);
            base.StateHasChanged();
        }
        else if (data != null)
        {
            Snackbar.Add("Create connector", Severity.Success);
            Dialog.Close(DialogResult.Ok(data.Id));
        }

        IsLoading = false;
        base.StateHasChanged();
    }

    public void HandleDialogClose()
    {
        Dialog.Cancel();
    }

}
