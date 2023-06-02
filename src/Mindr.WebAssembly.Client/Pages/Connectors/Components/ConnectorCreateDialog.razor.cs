using Microsoft.AspNetCore.Components;
using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.Models.DTO.Connector;
using MudBlazor;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Components;

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
        var response = await ConnectorClient.Insert(Connector);
        IsLoading = false;

        if(response.IsError())
        {
            var error = response.GetContent();
            Snackbar.Add(error, Severity.Error);
        }
        else if (response.IsSuccessful())
        {
            var data = response.GetContent<Connector>();
            Snackbar.Add("Create connector", Severity.Success);
            Dialog.Close(DialogResult.Ok(data!.Id));
        }

        IsLoading = false;
        base.StateHasChanged();
    }

    public void HandleDialogClose()
    {
        Dialog.Cancel();
    }

}
