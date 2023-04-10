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

namespace Mindr.WebUI.Views.Connectors.Components
{
    public partial class HttpItemSettings: FluentComponentBase
    {
        [Inject]
        public IHttpCollectionClient CollectionClient { get; set; } = default!;

        [Parameter, EditorRequired]
        public HttpItem Data { get; set; } = default!;

        [Parameter, EditorRequired]
        public Func<HttpItem, Task> OnChange { get; set; } = default!;

        [Parameter, EditorRequired]
        public Func<HttpItem, Task> OnUpdate { get; set; } = default!;

        public async Task OnHandleRun()
        {
            if (Data == null) return;

            Data = await CollectionClient.SendAsync(Data);

            await OnChange.Invoke(Data);
            base.StateHasChanged();
        }

        public async Task OnHandleUpdate()
        {
            if (Data == null) return;
            await OnUpdate.Invoke(Data);
            base.StateHasChanged();
        }

        public IEnumerable<HttpVariable>? GetItemRequestVariables()
        {
            return Data?.Request?.Variables;
        }

        public IEnumerable<HttpVariable>? GetItemResponseVariables()
        {
            var responses = Data?.Response;
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
            }

            return response?.Variables;
        }

    }
}
