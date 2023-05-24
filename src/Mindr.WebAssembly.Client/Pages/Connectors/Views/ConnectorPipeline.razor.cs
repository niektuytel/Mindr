using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;

using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.HttpRunner.Services;
using Mindr.WebAssembly.Client.Services;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using MudBlazor;
using Mindr.WebAssembly.Client.Pages.Connectors.Dialogs;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Views;

public partial class ConnectorPipeline
{
    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; } = default!;

    [Inject]
    public IHttpRunnerClient CollectionClient { get; set; } = default!;

    [Inject]
    public IHttpRunnerFactory CollectionFactory { get; set; } = default!;

    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    [Parameter, EditorRequired]
    public string ConnectorId { get; set; } = default!;

    public List<HttpItem> HttpItems { get; set; } = new List<HttpItem>();

    public HttpItem? SelectedHttpItem { get; set; } = null;

    public int SelectedIndex => SelectedHttpItem != null ? HttpItems.IndexOf(SelectedHttpItem) : 0;

    private HttpCollection Collection { get; set; } = new HttpCollection();

    //public HttpItemDialog HttpItemEditor = default!;

    private bool DataHasChanged = false;

    private bool IsLoading = false;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        var response = await ConnectorClient.Get(ConnectorId);
        (var connector, var error) = response.AsTuple();
        IsLoading = false;

        if (!string.IsNullOrEmpty(error))
        {
            Snackbar.Add(error, Severity.Error);
        }
        else if (connector != null)
        {
            HttpItems = connector!.Pipeline.ToList();
        }
    }

    public async Task HandleOnSave()
    {
        IsLoading = true;
        var response = await ConnectorClient.UpdatePipeline(ConnectorId, HttpItems.AsEnumerable());
        (_, var error) = response.AsTuple();
        IsLoading = false;

        if (!string.IsNullOrEmpty(error))
        {
            Snackbar.Add(error, Severity.Error);
            return;
        }

        DataHasChanged = false;
        base.StateHasChanged();
    }

    public async Task HandleHttpDrawerOpen()
    {
        // TODO: Open drawer 
        //var dialog = await DialogService.ShowAsync<HttpItemCreateDialog>();
        //var result = await dialog.Result;

        //if (!result.Canceled)
        //{
        //    if (Guid.TryParse(result.Data.ToString(), out Guid connectorId))
        //    {
        //        NavigationManager.NavigateTo($"/connectors/{connectorId}/pipeline");
        //    }
        //    else
        //    {
        //        Snackbar.Add($"Can't navigate, connector id is invalid", Severity.Error);
        //    }
        //}



        //IsLoading = true;

        ////var response = await ConnectorClient.UpdatePipeline(ConnectorId, HttpItems.AsEnumerable());
        ////(_, ErrorMessage) = response.AsTuple();
        ////if (!string.IsNullOrEmpty(ErrorMessage))
        ////{
        ////    base.StateHasChanged();
        ////}

        //IsLoading = false;
        //DataHasChanged = true;
        //base.StateHasChanged();
    }

    public async Task HandleOnRunAll()
    {
        // reload all results
        HttpItems.ForEach(item => item.IsLoading = true);
        HttpItems = await CollectionClient.SendAsync(HttpItems);

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
