using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Extensions;
using Mindr.Core.Interfaces;
using Mindr.Core.Models.HttpCollection;

namespace Mindr.WebUI.Components
{
    public partial class HttpPipeline: FluentComponentBase
    {

        [Inject]
        public IHttpCollectionClient CollectionClient { get; set; } = default!;

        public FluentDialog AddRequestDialog = default!;

        public HttpCollection Collection { get; set; } = new();

        public List<HttpItem> Pipeline { get; set; } = new() { _Constants.DefaultTestSample };

        public bool PipelineIsLoading { get; set; } = false;

        public int SelectedIndex { get; set; } = -1;

        public async Task OnItemRun()
        {
            PipelineIsLoading = true;

            // reload result
            Pipeline[SelectedIndex].Result = null;
            Pipeline[SelectedIndex] = await CollectionClient.SendAsync(Pipeline[SelectedIndex]);

            PipelineIsLoading = false;
        }

        public IEnumerable<HttpVariable>? GetResponseVariables()
        {
            if (SelectedIndex == -1) return null;
            var responses = Pipeline[SelectedIndex].Response;
            if (responses == null) return null;

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
    }
}
