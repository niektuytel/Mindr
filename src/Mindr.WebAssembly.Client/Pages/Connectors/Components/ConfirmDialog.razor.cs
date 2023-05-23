using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.WebAssembly.Client.Services;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Components;

public partial class ConfirmDialog
{
    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    [CascadingParameter]
    public MudDialogInstance Dialog { get; set; } = default!;

    public void HandleOnConfirm()
    {
        Dialog.Close();
    }

    public void HandleDialogClose()
    {
        Dialog.Cancel();
    }

}
