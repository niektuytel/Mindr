using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Extensions;
using Mindr.Core.Models.Connector.Http;
using Mindr.Core.Services.Connectors;
using Newtonsoft.Json;

namespace Mindr.WebUI.Components
{
    public partial class HttpPipeline: FluentComponentBase
    {
        [Parameter, EditorRequired]
        public List<HttpItem> Pipeline { get; set; } = default!;

        public HttpItem? SelectedItem { get; set; } = null;

        private int SelectedIndex => SelectedItem != null ? Pipeline.IndexOf(SelectedItem) : 0;

        [Inject]
        public IHttpCollectionClient CollectionClient { get; set; } = default!;

        [Inject]
        public IHttpCollectionFactory CollectionFactory { get; set; } = default!;

        public FluentDialog AddItemDialog = default!;

        protected async Task OnInitializedAsync()
        {
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                AddItemDialog.Hide();
            }
        }

        public async Task OnItemSelect(HttpItem item)
        {
            if (SelectedItem?.Id == item.Id) return;

            SelectedItem = item;
            base.StateHasChanged();
        }

        public async Task OnItemRemove(HttpItem item)
        {
            Pipeline.Remove(item);

            SelectedItem = Pipeline.Count() > 0 ? Pipeline.Last() : null;
            base.StateHasChanged();
        }

        public async Task OnItemRun()
        {
            if (SelectedItem == null) return;

            SelectedItem = await CollectionClient.SendAsync(SelectedItem);
            Pipeline[SelectedIndex] = SelectedItem;
            base.StateHasChanged();
        }
        
        public void OnHandleAdd(HttpItem item, IEnumerable<HttpVariable> globalVariables)
        {
            SelectedItem = item = CollectionFactory.PrepareHttpItem(item, Pipeline.AsEnumerable(), Collection);

            Pipeline.Add(item);
            CloseItemDialog();
            base.StateHasChanged();
        }

        public async Task OnHandleRun()
        {
            // reload all results
            Pipeline.ForEach(item => item.IsLoading = true);
            Pipeline = await CollectionClient.SendAsync(Pipeline);
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
                    foreach (var pipeItem in Pipeline)
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
