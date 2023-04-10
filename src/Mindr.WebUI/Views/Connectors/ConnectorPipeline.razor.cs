using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Extensions;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Mindr.Core.Services.Connectors;
using Mindr.WebUI.Services.ApiClients;
using Mindr.WebUI.Views.Connectors.Components;
using Newtonsoft.Json;

namespace Mindr.WebUI.Views.Connectors
{
    public partial class ConnectorPipeline: FluentComponentBase
    {
        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; } = default!;

        [Inject]
        public IHttpCollectionClient CollectionClient { get; set; } = default!;

        [Inject]
        public IHttpCollectionFactory CollectionFactory { get; set; } = default!;

        [Parameter, EditorRequired]
        public string ConnectorId { get; set; } = default!;

        public List<HttpItem> HttpItems { get; set; } = new List<HttpItem>();

        public HttpItem? SelectedHttpItem { get; set; } = null;

        public int SelectedIndex => SelectedHttpItem != null ? HttpItems.IndexOf(SelectedHttpItem) : 0;

        private HttpCollection Collection { get; set; } = new HttpCollection();
        
        public HttpItemDialog HttpItemEditor = default!;

        private bool DataHasChanged = false;
        
        private bool IsLoading = false;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await OnPipelineLoad();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task OnPipelineLoad()
        {
            if (string.IsNullOrEmpty(ConnectorId))
            {
                return;
            }

            IsLoading = true;

            var response = await ConnectorClient.Get(ConnectorId);
            if (response == null)
            {
                // Failed request
                throw new NotImplementedException();
            }

            var json = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(json))
            {
                var connector = JsonConvert.DeserializeObject<Connector>(json);
                HttpItems = connector.Pipeline.ToList();
            }

            IsLoading = false;
            base.StateHasChanged();
        }

        public async Task OnHandleSave()
        {
            IsLoading = true;
            
            var response = await ConnectorClient.UpdateHttpItems(ConnectorId, HttpItems.AsEnumerable());
            if (response?.IsSuccessStatusCode != true)
            {
                // Failed request
                throw new NotImplementedException();
            }

            IsLoading = false;
            DataHasChanged = false;
            base.StateHasChanged();
        }

        public async Task OnHandleCreate(HttpItem item)
        {
            IsLoading = true;
            SelectedHttpItem = CollectionFactory.PrepareHttpItem(item, HttpItems.AsEnumerable(), Collection);
            HttpItems.Add(SelectedHttpItem);
            IsLoading = false;

            DataHasChanged = true;
            base.StateHasChanged();
        }

        public async Task OnHandleUpdate(HttpItem item)
        {
            IsLoading = true;
            SelectedHttpItem = CollectionFactory.PrepareHttpItem(item, HttpItems.AsEnumerable(), Collection);
            HttpItems[SelectedIndex] = SelectedHttpItem;
            IsLoading = false;

            DataHasChanged = true;
            base.StateHasChanged();
        }

        public async Task OnHandleRun()
        {
            // reload all results
            HttpItems.ForEach(item => item.IsLoading = true);
            HttpItems = await CollectionClient.SendAsync(HttpItems);
        }

        public async Task OnHandleChange(HttpItem item)
        {
            if (item == null) return;
            if (SelectedHttpItem?.Id == item.Id) return;

            SelectedHttpItem = item;

            base.StateHasChanged();
        }

        public async Task OnHandleRemove(HttpItem item)
        {
            HttpItems.Remove(item);
            SelectedHttpItem = HttpItems.Count() > 0 ? HttpItems.Last() : null;

            DataHasChanged = true;
            base.StateHasChanged();
        }

        public async Task OnOpenEditor(HttpItem item)
        {
            if (item == null) return;
            HttpItemEditor.OpenEditDialog(item);
            base.StateHasChanged();
        }

    }
}
