using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Domain.HttpRunner.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Drawers;

// Usefull to use? https://www.postman.com/cs-demo/workspace/public-rest-apis/overview
public partial class HttpItemDrawer
{
    [Parameter, EditorRequired]
    public Func<HttpItem, Task> OnCreate { get; set; } = default!;

    [Parameter, EditorRequired]
    public Func<HttpItem, Task> OnUpdate { get; set; } = default!;

    private HttpItem Item = new();

    public string NewHeaderKey { get; set; } = "";

    public string Title { get; set; } = "";

    private bool DataHasChanged = false;

    private bool Open = false;

    private bool success;

    private async Task HandleOnHeaderAdd(FocusEventArgs args)
    {
        if (string.IsNullOrEmpty(NewHeaderKey)) return;

        var headers = Item.Request.Header.Where(item => !string.IsNullOrEmpty(item.Key) || !string.IsNullOrEmpty(item.Value)).ToList();
        headers.Add(new HttpHeader() { Key = NewHeaderKey, Value = "" });
        Item.Request.Header = headers;

        NewHeaderKey = "";
        //StateHasChanged();
    }

    public void HandleOpenOnCreate()
    {
        Item = new();
        Title = "Create HTTP";
        Open = true;
    }

    public void HandleOpenOnUpdate(HttpItem item)
    {
        Item = item;
        Title = "Update HTTP";
        Open = true;
    }

    public async Task HandleOnConfirm()
    {
        if (Item == null) return;

        if (Title.ToLower().Contains("create"))
        {
            await OnCreate.Invoke(Item);

        }
        else if (Title.ToLower().Contains("update"))
        {
            await OnUpdate.Invoke(Item);

        }

        Open = false;
    }

    public void HandleOnCancel()
    {
        Open = false;
    }



}
