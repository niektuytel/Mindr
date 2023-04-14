using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.Connectors;
using Mindr.WebUI.Pages.Connectors.Components;
using Mindr.WebUI.Services;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Mindr.WebUI.Pages.Connectors.Views
{
    public partial class ConnectorsView : FluentComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; }

        private bool IsLoadingData = false;
        private bool IsLoadingDialog = false;

        private readonly Connector AddItemData = new();

        public ConnectorDialog Dialog = default!;

        public int ItemsPerPage { get; set; } = 10;

        public PaginationState Pagination { get; set; } = new() { ItemsPerPage = 10 };

        private GridItemsProviderRequest<Connector> DataProviderRequest { get; set; } = default!;

        private GridItemsProvider<Connector> DataProvider { get; set; } = default!;

        private ICollection<Connector>? DataCollection { get; set; } = new Collection<Connector>();

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
                    // Failed request
                    throw new NotImplementedException();
                }

                var json = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(json))
                {
                    DataCollection = JsonConvert.DeserializeObject<ICollection<Connector>>(json);
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
            if (response == null)
            {
                // Failed request
                throw new NotImplementedException();
            }

            var json = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(json))
            {
                var data = JsonConvert.DeserializeObject<Connector>(json);
                NavigationManager.NavigateTo($"/connectors/{data!.Id}/pipeline");
            }
            else
            {
                // TODO: create error message
            }

            //IsLoading = false;
            //HandleDialogClose();
            base.StateHasChanged();
        }

        private void HandleRowClick(Connector? item)
        {
            if (item == null) return;

            NavigationManager.NavigateTo($"/connectors/{item!.Id}/overview");

            base.StateHasChanged();
        }

        private async Task HandleCellFocus(FluentDataGridCell<Connector> cell)
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
