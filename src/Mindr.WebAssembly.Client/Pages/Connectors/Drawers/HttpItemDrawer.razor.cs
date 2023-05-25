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

    [Parameter, EditorRequired]
    public HttpItem? Item { get; set; } = default!;

    public string NewHeaderKey { get; set; } = "";

    public string ButtonText { get; set; } = "";

    //public FluentDialog Dialog = default!;

    private bool IsLoading = false;

    //protected override Task OnAfterRenderAsync(bool firstRender)
    //{
    //    if (firstRender)
    //    {
    //        Dialog.Hide();
    //    }

    //    return base.OnAfterRenderAsync(firstRender);
    //}

    public async Task HandleOnSave()
    {
        IsLoading = true;

        if (ButtonText == "Create")
        {
            await OnCreate.Invoke(Item);

        }
        else if (ButtonText == "Update")
        {
            await OnUpdate.Invoke(Item);

        }
        else
        {
            throw new NotImplementedException($"{ButtonText} does not exist");
        }


        IsLoading = false;
        CloseDialog();
        //StateHasChanged();
    }

    private async Task HandleOnHeaderAdd(FocusEventArgs args)
    {
        if (string.IsNullOrEmpty(NewHeaderKey)) return;

        var headers = Item.Request.Header.Where(item => !string.IsNullOrEmpty(item.Key) || !string.IsNullOrEmpty(item.Value)).ToList();
        headers.Add(new HttpHeader() { Key = NewHeaderKey, Value = "" });
        Item.Request.Header = headers;

        NewHeaderKey = "";
        //StateHasChanged();
    }

    public void DismissDialog(DialogEventArgs args)
    {
        if (args is not null && args.Reason is not null && args.Reason == "dismiss")
        {
            //Dialog.Hide();
        }
    }

    public void CloseDialog()
    {
        //Dialog.Hide();
    }

    public void OpenAddDialog()
    {
        Item = new();
        ButtonText = "Create";
        //if (Collection == null)
        //{// TODO: 
        //    //Collection = JsonConvert.DeserializeObject<HttpCollection>(_Constants.Json);
        //}

        //Dialog.Show();
    }

    public void OpenEditDialog(HttpItem item)
    {
        Item = item;
        ButtonText = "Update";
        //if (Collection == null)
        //{// TODO: 
        //    //Collection = JsonConvert.DeserializeObject<HttpCollection>(_Constants.Json);
        //}

        //Dialog.Show();
    }
}
