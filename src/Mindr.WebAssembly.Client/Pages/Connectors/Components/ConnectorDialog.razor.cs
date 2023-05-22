using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.Models.DTO.Connector;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MudBlazor;
using System;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Components;

public partial class ConnectorDialog
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
