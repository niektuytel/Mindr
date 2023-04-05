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

namespace Mindr.WebUI.Views.ConnectorViews
{
    // Usefull to use? https://www.postman.com/cs-demo/workspace/public-rest-apis/overview
    public partial class ConnectorPipeline: FluentComponentBase
    {
        [Parameter, EditorRequired]
        public string ConnectorId { get; set; }

        public HttpItem? SelectedItem { get; set; } = null;

        private int SelectedIndex => SelectedItem != null ? HttpItems.IndexOf(SelectedItem) : 0;

        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; } = default!;

        [Inject]
        public IHttpCollectionClient CollectionClient { get; set; } = default!;

        [Inject]
        public IHttpCollectionFactory CollectionFactory { get; set; } = default!;

        public FluentDialog AddItemDialog = default!;

        private bool IsLoading = false;

        private HttpItem NewHttpItem { get; set; } = new HttpItem() { 
            Request = new HttpRequest() { 
                Url = new HttpRequestUrl(),
                Header = new List<HttpHeader>(),
                Body = new HttpBody()
            } 
        };

        private List<HttpItem>? HttpItems { get; set; } = null;
        //public List<HttpItem> HttpItems { get; set; } = new() { _Constants.DefaultTestSample, _Constants.DefaultTestSample2 };

        
        static List<Option<string>> RequestMethods = new()
        {
            { new Option<string> { Value = "GET", Text = "GET", Selected = true } },
            { new Option<string> { Value = "POST", Text = "POST" } },
            { new Option<string> { Value = "PUT", Text = "PUT" } },
            { new Option<string> { Value = "DELETE", Text = "DELETE" } }
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await OnPipelineLoad();
                AddItemDialog.Hide();
            }

            await base.OnAfterRenderAsync(firstRender);
        }


        public string NewHeaderKey { get; set; } = "";

        private async Task OnInsertHeaderLine(FocusEventArgs args)
        {
            var headers = NewHttpItem.Request.Header.ToList();
            headers.Add(new HttpHeader() { Key = NewHeaderKey, Value = "" });
            NewHttpItem.Request.Header = headers;

            NewHeaderKey = "";
            base.StateHasChanged();
        }

        private async Task OnPipelineLoad()
        {
            if (string.IsNullOrEmpty(ConnectorId))
            {
                return;
            }

            IsLoading = true;

            var response = await ConnectorClient.GetById(ConnectorId);
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

        public async Task OnItemSelect(HttpItem item)
        {
            if (SelectedItem?.Id == item.Id) return;

            SelectedItem = item;
            base.StateHasChanged();
        }

        public async Task OnItemRemove(HttpItem item)
        {
            HttpItems.Remove(item);

            SelectedItem = HttpItems.Count() > 0 ? HttpItems.Last() : null;
            base.StateHasChanged();
        }

        public async Task OnItemRun()
        {
            if (SelectedItem == null) return;

            SelectedItem = await CollectionClient.SendAsync(SelectedItem);
            HttpItems[SelectedIndex] = SelectedItem;
            base.StateHasChanged();
        }
        
        public void OnHandleAdd(HttpItem item, IEnumerable<HttpVariable> globalVariables)
        {
            SelectedItem = item = CollectionFactory.PrepareHttpItem(item, HttpItems.AsEnumerable(), Collection);

            HttpItems.Add(item);
            CloseItemDialog();
            base.StateHasChanged();
        }

        public async Task OnHandleRun()
        {
            // reload all results
            HttpItems.ForEach(item => item.IsLoading = true);
            HttpItems = await CollectionClient.SendAsync(HttpItems);
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
            return SelectedItem?.Request?.Variables;
        }

        public IEnumerable<HttpVariable>? GetItemResponseVariables()
        {
            var responses = SelectedItem?.Response;
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
            if (Collection == null)
            {
                Collection = JsonConvert.DeserializeObject<HttpCollection>(_Constants.Json);
            }

            AddItemDialog.Show();
        }
    }
}
