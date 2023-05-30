using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;

using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.HttpRunner.Services;
using Mindr.Domain.HttpRunner.Extensions;
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

    private MudDropContainer<DropItem>? Container = default!;


    private bool DataHasChanged = false;

    private bool IsLoading = false;

    private bool success;

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
        }

        base.StateHasChanged();
        Container.Refresh();
    }

    public async Task HandleOnSave()
    {
        IsLoading = true;
        var response = await ConnectorClient.UpdatePipeline(ConnectorId, Items.AsEnumerable());
        (var items, var error) = response.AsTuple();
        IsLoading = false;

        if (!string.IsNullOrEmpty(error))
        {
            Snackbar.Add(error, Severity.Error);
            return;
        }

        Items = items.Select(item => new DropItem(item)).ToList();

        DataHasChanged = false;
        Container!.Refresh();
        base.StateHasChanged();
    }

    public async Task HandleOnRunAll()
    {
        // reload all results
        Items.ForEach(item => item.IsLoading = true);

        var items = (Items as IEnumerable<HttpItem>).ToList();
        items = await CollectionClient.SendAsync(items);
        Items = items.Select(item => new DropItem(item)).ToList();

        Container!.Refresh();
        base.StateHasChanged();
    }

    public async Task HandleOnCreateItem(HttpItem item)
    {
        SelectedItem = new DropItem(CollectionFactory.PrepareHttpItem(item, Items.AsEnumerable() as IEnumerable<HttpItem>, new HttpCollection()));
        Items.Add(SelectedItem);
        
        DataHasChanged = true;
        Container!.Refresh();
        base.StateHasChanged();
    }

    public async Task HandleOnUpdateItem(HttpItem item)
    {
        var httpItem = CollectionFactory.PrepareHttpItem(item, Items.AsEnumerable() as IEnumerable<HttpItem>, new HttpCollection());
        SelectedItem = new DropItem(httpItem);
        Items = Items.Select(elem => elem.Id == item.Id ? SelectedItem : elem).ToList();

        DataHasChanged = true;
        Container!.Refresh();
        base.StateHasChanged();
    }

    public async Task HandleOnRemoveItem(HttpItem item)
    {
        var dropItem = Items.FirstOrDefault(elem => elem.Id == item.Id);
        if (dropItem == null) return;

        Items.Remove(dropItem);
        SelectedItem = Items.Count() > 0 ? Items.Last() : null;

        DataHasChanged = true;
        Container!.Refresh();
        base.StateHasChanged();
    }

    public async Task HandleOnSelectItem(HttpItem item)
    {
        if (item == null) return;
        if (SelectedItem?.Id == item.Id) return;
        SelectedItem = new DropItem(item);

        Container!.Refresh();
        base.StateHasChanged();
    }

    public async Task HandleOnRunItem(HttpItem item)
    {
        // reload all results
        item.IsLoading = true;
        item = await CollectionClient.SendAsync(item);
        Items = Items.Select(elem => elem.Id == item.Id ? new DropItem(item) : elem).ToList();

        Container!.Refresh();
    }

    private void ItemDropped(MudItemDropInfo<DropItem> dropItem)
    {
        if (dropItem?.Item == null) return;

        dropItem.Item.Identifier = dropItem.DropzoneIdentifier;
        DataHasChanged = true;
    }

    public List<HttpVariable> GetItemResponseVariables()
    {
        var result = new List<HttpVariable>();
        var responses = SelectedItem?.Response;
        if (responses == null) return result;

        // TODO: Add more options to respond on: [201, 302, 404, 500, etc.]
        var response = responses.FirstOrDefault(item => item.Code == 200);
        if (response != null && response.Variables != null)
        {
            result = response.GetVariables().ToList();
        }

        return result;
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

    public string Identifier { get; set; } = "dropzone1";
}
