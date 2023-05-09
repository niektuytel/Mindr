using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.WebAssembly.Client.Pages.Connectors.Components;
using Mindr.WebAssembly.Client.Services;
using System.Text.Json.Serialization;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.JSInterop;
using Mindr.Domain.Models.DTO.Connector;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Views;

public partial class ConnectorsView : FluentComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; }

    private string? ErrorMessage { get; set; }

    private bool IsLoadingData = false;
    private bool IsLoadingDialog = false;

    private readonly Connector AddItemData = new();

    public ConnectorDialog Dialog = default!;

    public int ItemsPerPage { get; set; } = 10;

    public PaginationState Pagination { get; set; } = new() { ItemsPerPage = 10 };

    private GridItemsProviderRequest<Connector> DataProviderRequest { get; set; } = default!;

    private GridItemsProvider<ConnectorBriefDTO> DataProvider { get; set; } = default!;

    private ICollection<ConnectorBriefDTO>? DataCollection { get; set; } = new Collection<ConnectorBriefDTO>();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            OnConnectorLoad();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private void OnConnectorLoad()
    {
        Pagination = new PaginationState { ItemsPerPage = ItemsPerPage };
        DataProvider = async req =>
        {
            IsLoadingData = true;

            var response = await ConnectorClient.GetAll();
            (DataCollection, ErrorMessage) = response.AsTuple();
            if(!string.IsNullOrEmpty(ErrorMessage))
            {
                base.StateHasChanged();
            }

            // +1 to add space to to 1 filled space,
            // The provide now knows that he need to add a next page
            var total = (DataProviderRequest.StartIndex + (DataCollection!.Count + 1));

            IsLoadingData = false;
            return GridItemsProviderResult.From(DataCollection, total);
        };

        base.StateHasChanged();
    }

    public async Task OnConnectorAdd()
    {
        IsLoadingData = true;

        var response = await ConnectorClient.Create(AddItemData);
        (var data, ErrorMessage) = response.AsTuple();
        if (data != null)
        {
            NavigationManager.NavigateTo($"/connectors/{data!.Id}/pipeline");
        }
        else if (!string.IsNullOrEmpty(ErrorMessage))
        {
            base.StateHasChanged();
        }

        IsLoadingData = false;
        base.StateHasChanged();
    }

    private void HandleRowClick(ConnectorBriefDTO? item)
    {
        if (item == null) return;

        NavigationManager.NavigateTo($"/connectors/{item!.Id}/overview");

        base.StateHasChanged();
    }

    private async Task HandleCellFocus(FluentDataGridCell<ConnectorBriefDTO> cell)
    {
        if (cell.CellType == DataGridCellType.Default)
        {
            var value = cell.Owner.AdditionalAttributes?.GetValueOrDefault("aria-rowindex");
            if (value != null)
            {
                var index = (System.Index)((int)value - 2);
                var item = DataCollection?.ElementAtOrDefault(index);
                HandleRowClick(item);
            }
        }

        base.StateHasChanged();
    }

    public void HandleDialogOpen()
    {

        Dialog.HandleDialogOpen();
    }

    public void HandleDialogClose()
    {
        Dialog.HandleDialogClose();
    }

    public void HandleDialogDismiss(DialogEventArgs args)
    {
        if (args is not null && args.Reason is not null && args.Reason == "dismiss")
        {
            Dialog.HandleDialogClose();
        }
    }
}
