
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Enums;
using Mindr.Core.Extensions;
using Mindr.Core.Interfaces;
using Mindr.Core.Models.HttpCollection;
using Mindr.WebUI.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Mindr.WebUI.Pages;

public partial class ConnectorPage : FluentComponentBase
{
    [Inject]
    public IHttpCollectionClient CollectionClient { get; set; } = default!;

    public FluentDialog AddRequestDialog = default!;

    public HttpCollection Collection { get; set; } = new();
    
    public List<HttpItem> Pipeline { get; set; } = new() { _Constants.DefaultTestSample };

    public bool PipelineIsLoading { get; set; } = false;

    public int SelectedIndex { get; set; } = -1;

    protected async Task OnInitializedAsync()
    {



    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            AddRequestDialog.Hide();
        }
    }

    public void OnItemSelect(int index)
    {
        SelectedIndex = index;

        base.StateHasChanged();
    }

    public void OnItemRemove(HttpItem item)
    {
        Pipeline.Remove(item);

        SelectedIndex = -1;
        base.StateHasChanged();
    }

    public async Task OnItemRun()
    {
        PipelineIsLoading = true;

        // reload result
        Pipeline[SelectedIndex].Result = null;
        Pipeline[SelectedIndex] = await CollectionClient.SendAsync(Pipeline[SelectedIndex]);

        PipelineIsLoading = false;
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
                if(res != null)
                {
                    variable.Value = res.Value;
                    break;
                }
            }

            // set global variable to this call
            if(string.IsNullOrEmpty(variable.Value))
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

    public IEnumerable<HttpVariable>? GetResponseVariables()
    {
        if(SelectedIndex == -1) return null;
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

    public void OpenRequestDialog()
    {
        if (Collections.Count() == 0)
        {
            // TODO: need to call as brief output from api
            var collection = JsonConvert.DeserializeObject<HttpCollection>(_Constants.Json);
            Collections.Add(collection);
        }

        AddRequestDialog.Show();
    }

    public void CloseRequestDialog()
    {
        AddRequestDialog.Hide();

    }

    public void DismissRequestDialog(DialogEventArgs args)
    {
        if (args is not null && args.Reason is not null && args.Reason == "dismiss")
        {
            AddRequestDialog.Hide();
        }
    }


}
