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
using System.ComponentModel.DataAnnotations.Schema;
using Mindr.WebAssembly.Client.Pages.Connectors.Drawers;

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

    public List<DropItem> Items { get; set; } = new List<DropItem>();

    public DropItem? SelectedItem { get; set; } = null;

    public int TransactionIndex = 0;

    private HttpItemDrawer? ItemDrawer = default!;

    //public HttpItemDialog HttpItemEditor = default!;

    private MudDropContainer<DropItem> Container = default!;


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
            Items = connector!.Pipeline.Select(item => new DropItem(item)).ToList();
            Container.Refresh();
        }
    }

    public async Task HandleOnSave()
    {
        IsLoading = true;
        var response = await ConnectorClient.UpdatePipeline(ConnectorId, Items.AsEnumerable());
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

    public async Task HandleOnRunAll()
    {
        // reload all results
        Items.ForEach(item => item.IsLoading = true);

        var items = (Items as IEnumerable<HttpItem>).ToList();
        items = await CollectionClient.SendAsync(items);
        Items = items.Select(item => new DropItem(item)).ToList();

        base.StateHasChanged();
    }

    public async Task HandleOnCreateItem(HttpItem item)
    {
        var dropItem = new DropItem(CollectionFactory.PrepareHttpItem(item, Items.AsEnumerable() as IEnumerable<HttpItem>, new HttpCollection()));
        Items.Add(dropItem);
        
        DataHasChanged = true;
        Container.Refresh();
        base.StateHasChanged();
    }

    public async Task HandleOnUpdateItem(HttpItem item)
    {
        var dropItem = new DropItem(CollectionFactory.PrepareHttpItem(item, Items.AsEnumerable() as IEnumerable<HttpItem>, new HttpCollection()));
        Items = Items.Select(elem => elem.Id == item.Id ? dropItem : elem).ToList();

        DataHasChanged = true;
        Container.Refresh();
        base.StateHasChanged();
    }



    public async Task HandleOnEditItem(HttpItem item)
    {

    }



    public async Task HandleOnSelectItem(HttpItem item)
    {
        if (item == null) return;
        if (SelectedItem?.Id == item.Id) return;

        SelectedItem = new DropItem(item);
        base.StateHasChanged();
    }

    public async Task HandleOnRemoveItem(HttpItem item)
    {
        Items.Remove(new DropItem(item));
        SelectedItem = Items.Count() > 0 ? Items.Last() : null;

        DataHasChanged = true;
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

    public async Task OnOpenEditor(HttpItem item)
    {
        if (item == null) return;
        //HttpItemEditor.OpenEditDialog(item);
        base.StateHasChanged();
    }


    private bool HasEmptyVariable(DropItem item)
    {
        if (item.Request.Variables == null) return false;

        var emptyVars = item.Request.Variables.Where(value => string.IsNullOrEmpty(value.Value));
        return emptyVars.Any();
    }

    private string GetMethodStyle(DropItem item)
    {
        var style = "border-right: ridge;padding-right:5px;margin-right:5px;font-weight: bold;";
        var value = item.Request.Method.ToLower();
        if (value == "get")
        {
            return $"{style}color: green;";
        }

        if (value == "post")
        {
            return $"{style}color: orange;";
        }

        if (value == "delete")
        {
            return $"{style}color: red;";
        }

        return style;
    }

    private string GetStatusCodeStyle(DropItem item)
    {
        if (item.Result == null) return "";

        return item.Result.IsSuccessStatusCode ? "highlight" : "lowlight";
    }


}
public class DropItem: HttpItem
{
    public DropItem(HttpItem item)
    {
        Id = item.Id;
        IsLoading = item.IsLoading;
        Result = item.Result;
        Items = item.Items;
        Name = item.Name;
        Description = item.Description;
        Request = item.Request;
        Response = item.Response;
    }

    public bool IsSelected { get; set; } = false;
    //public bool TransactionIndex { get; set; } = 0;
    public string Identifier { get; set; } = "dropzone1";
}
