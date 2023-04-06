using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Extensions;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Mindr.Core.Services.Connectors;
using Mindr.WebUI.Services.ApiClients;
using Newtonsoft.Json;

namespace Mindr.WebUI.Views.Connectors
{
    // Usefull to use? https://www.postman.com/cs-demo/workspace/public-rest-apis/overview
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

        public int SelectedIndex => SelectedHttpItem != null ? HttpItems.IndexOf(SelectedHttpItem) : 0;

        public HttpItem? SelectedHttpItem { get; set; } = null;
        
        private HttpItem NewHttpItem { get; set; } = new HttpItem();

        public FluentDialog AddItemDialog = default!;

        private bool HasChangedData = false;
        
        private bool IsLoading = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await OnPipelineLoad();
                AddItemDialog.Hide();
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
            base.StateHasChanged();
        }

        public async Task OnHandleAdd()
        {
            IsLoading = true;
            SelectedHttpItem = NewHttpItem = CollectionFactory.PrepareHttpItem(NewHttpItem, HttpItems.AsEnumerable(), Collection);

            HttpItems.Add(NewHttpItem);
            NewHttpItem = new();

            CloseItemDialog();
            IsLoading = false;
            base.StateHasChanged();
        }

        public async Task OnHandleRun()
        {
            // reload all results
            HttpItems.ForEach(item => item.IsLoading = true);
            HttpItems = await CollectionClient.SendAsync(HttpItems);
        }





        public string NewHeaderKey { get; set; } = "";

        private async Task OnInsertHeaderLine(FocusEventArgs args)
        {
            if (string.IsNullOrEmpty(NewHeaderKey)) return;

            var headers = NewHttpItem.Request.Header.Where(item => !string.IsNullOrEmpty(item.Key) || !string.IsNullOrEmpty(item.Value)).ToList();
            headers.Add(new HttpHeader() { Key = NewHeaderKey, Value = "" });
            NewHttpItem.Request.Header = headers;

            NewHeaderKey = "";
            base.StateHasChanged();
        }

        public async Task OnItemSelect(HttpItem item)
        {
            if (SelectedHttpItem?.Id == item.Id) return;

            SelectedHttpItem = item;
            base.StateHasChanged();
        }

        public async Task OnItemEdit(HttpItem item)
        {
            throw new NotImplementedException();
            //HttpItems.Remove(SelectedHttpItem);

            //SelectedHttpItem = HttpItems.Count() > 0 ? HttpItems.Last() : null;
            base.StateHasChanged();
        }
        public async Task OnItemRemove(HttpItem item)
        {
            HttpItems.Remove(item);

            SelectedHttpItem = HttpItems.Count() > 0 ? HttpItems.Last() : null;
            base.StateHasChanged();
        }

        public async Task OnItemRun()
        {
            if (SelectedHttpItem == null) return;

            SelectedHttpItem = await CollectionClient.SendAsync(SelectedHttpItem);
            HttpItems[SelectedIndex] = SelectedHttpItem;
            base.StateHasChanged();
        }
        
        public void CloseItemDialog()
        {
            AddItemDialog.Hide();
        }

        public void DismissItemDialog(DialogEventArgs args)
        {
            if (args is not null && args.Reason is not null && args.Reason == "dismiss")
            {
                AddItemDialog.Hide();
            }
        }

        public IEnumerable<HttpVariable>? GetItemRequestVariables()
        {
            return SelectedHttpItem?.Request?.Variables;
        }

        public IEnumerable<HttpVariable>? GetItemResponseVariables()
        {
            var responses = SelectedHttpItem?.Response;
            if(responses == null) return null;

            // TODO: Add more options to respond on: [201, 302, 404, 500, etc.]
            var response = responses.FirstOrDefault(item => item.Code == 200);
            if (response != null)
            {
                // set item variables
                if (response.Variables == null)
                {
                    response.Variables = response.GetVariables();
                }

                foreach (var variable in response.Variables)
                {
                    // set other matching variables to this call
                    foreach (var pipeItem in HttpItems)
                    {
                        var res = response.Variables.FirstOrDefault(i => (i.Key == variable.Key && !string.IsNullOrEmpty(i.Value)));
                        if (res != null)
                        {
                            variable.Value = res.Value;
                            break;
                        }
                    }
                }
            }

            return response?.Variables;
        }

        // TODO: Use API with brief data of collections
        public HttpCollection? Collection { get; set; } = null;

        public void OpenItemDialog()
        {
            //if (Collection == null)
            //{
            //    //Collection = JsonConvert.DeserializeObject<HttpCollection>(_Constants.Json);
            //}

            AddItemDialog.Show();
        }
    }
}
