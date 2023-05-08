using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.HttpRunner.Models;
using Mindr.HttpRunner.Services;
using Mindr.HttpRunner.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Mindr.Client.Pages.Connectors.Components
{
    public partial class HttpItemSettings : FluentComponentBase
    {
        [Inject]
        public IHttpRunnerClient CollectionClient { get; set; } = default!;

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
            }

            return response?.Variables;
        }

    }
}
