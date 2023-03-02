using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Extensions;
using Mindr.Core.Interfaces;
using Mindr.Core.Models.HttpCollection;

namespace Mindr.WebUI.Components
{
    public partial class HttpPipelineItem: FluentComponentBase
    {

        [Parameter, EditorRequired]
        public HttpItem Data { get; set; } = default!;


        [Inject]
        public IHttpCollectionClient CollectionClient { get; set; } = default!;

        public FluentDialog AddRequestDialog = default!;

        public HttpCollection Collection { get; set; } = new();

        public List<HttpItem> Pipeline { get; set; } = new() { _Constants.DefaultTestSample };

        public bool PipelineIsLoading { get; set; } = false;

        public int SelectedIndex { get; set; } = -1;

        public void OnHandleSelect(int index)
        {
            SelectedIndex = index;

            base.StateHasChanged();
        }

        public void OnHandleRemove(HttpItem item)
        {
            Pipeline.Remove(item);

            SelectedIndex = -1;
            base.StateHasChanged();
        }

        public void OnPipelineAdd(HttpItem item, HttpVariable[] globalVariables)
        {
            // TODO: set global key on global values that are set in requests
            // set item variables
            if (item.Request.Variables == null)
            {
                item.Request.Variables = item.Request.GetVariables();
            }

            foreach (var variable in item.Request.Variables)
            {
                // set other matching variables to this call
                foreach (var pipeItem in Pipeline)
                {
                    var res = pipeItem.Request.Variables.FirstOrDefault(i => (i.Key == variable.Key && !string.IsNullOrEmpty(i.Value)));
                    if (res != null)
                    {
                        variable.Value = res.Value;
                        break;
                    }
                }

                // set global variable to this call
                if (string.IsNullOrEmpty(variable.Value))
                {
                    var res = globalVariables.FirstOrDefault(i => (i.Key == variable.Key && !string.IsNullOrEmpty(i.Value)));
                    if (res != null)
                    {
                        variable.Value = res.Value;
                    }
                }
            }


            Pipeline.Add(item);

            SelectedIndex++;
            AddRequestDialog!.Hide();
            base.StateHasChanged();
        }

        public void OnPipelineAdd(IEnumerable<HttpItem> items)
        {
            Pipeline.AddRange(items);

            SelectedIndex++;
            base.StateHasChanged();
        }

        public async Task OnPipelineRun()
        {
            PipelineIsLoading = true;

            // reload all results
            Pipeline.ForEach(item => item.Result = null);
            Pipeline = await CollectionClient.SendAsync(Pipeline);

            PipelineIsLoading = false;
        }



    }
}
