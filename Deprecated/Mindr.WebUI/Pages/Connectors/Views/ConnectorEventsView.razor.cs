using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.ConnectorEvents;
using Mindr.Core.Models.Connectors;
using Mindr.HttpRunner.Models;
using Mindr.Client.Pages.Connectors.Components;
using Mindr.Client.Services;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Mindr.Client.Pages.Connectors.Views
{
    public partial class ConnectorEventsView : FluentComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; }

        private bool IsLoadingData = false;
        private bool IsLoadingDialog = false;
        private string? ErrorMessage { get; set; }

        private readonly Connector AddItemData = new();

        public ConnectorDialog Dialog = default!;

        public int ItemsPerPage { get; set; } = 10;

        public PaginationState Pagination { get; set; } = new() { ItemsPerPage = 10 };

        private GridItemsProviderRequest<ConnectorOverviewDTO> DataProviderRequest { get; set; } = default!;

        private GridItemsProvider<ConnectorOverviewDTO> DataProvider { get; set; } = default!;

        private ICollection<ConnectorOverviewDTO>? DataCollection { get; set; } = new Collection<ConnectorOverviewDTO>();

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
                if (response == null)
                {
                    // TODO: should be fixed with refresh token

                    ErrorMessage = $"Login session expired, Please login again";
                    base.StateHasChanged();
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        DataCollection = JsonConvert.DeserializeObject<ICollection<ConnectorOverviewDTO>>(content);
                    }
                    else
                    {
                        ErrorMessage = content;
                        base.StateHasChanged();
                    }
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
            IsLoadingData = false;

            if (response == null)
            {
                // TODO: should be fixed with refresh token

                ErrorMessage = $"Login session expired, Please login again";
                base.StateHasChanged();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<ConnectorEvent>(content);
                    NavigationManager.NavigateTo($"/connectors/{data!.Id}/pipeline");
                }
                else
                {
                    ErrorMessage = content;
                    base.StateHasChanged();
                }
            }

            //HandleDialogClose();
            base.StateHasChanged();
        }

        private void HandleRowClick(ConnectorOverviewDTO? item)
        {
            if (item == null) return;

            NavigationManager.NavigateTo($"/connectors/{item!.Id}/overview");

            base.StateHasChanged();
        }

        private async Task HandleCellFocus(FluentDataGridCell<ConnectorOverviewDTO> cell)
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
}
