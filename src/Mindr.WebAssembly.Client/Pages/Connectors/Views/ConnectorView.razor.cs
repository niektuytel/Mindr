using Microsoft.AspNetCore.Components;
using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.Models.DTO.Connector;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MudBlazor;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Views;

public partial class ConnectorView
{
    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    [Parameter, EditorRequired]
    public string ConnectorId { get; set; } = default!;

    [Parameter, EditorRequired] 
    public string? NavName { get; set; } = default!;

    private bool IsLoading = false;

    private ConnectorOverviewDTO Overview { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        var response = await ConnectorClient.GetOverview(ConnectorId);
        IsLoading = false;

        if (response.IsError())
        {
            var error = response.GetContent();
            Snackbar.Add(error, Severity.Error);
        }
        else if (response.IsSuccessful())
        {
            Overview = response.GetContent<ConnectorOverviewDTO>();
        }

        base.StateHasChanged();
    }
}
