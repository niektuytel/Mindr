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
    // Usefull to use? https://www.postman.com/cs-demo/workspace/public-rest-apis/overview
    public partial class HttpItemDialog: FluentComponentBase
    {
        [Parameter, EditorRequired]
        public Func<HttpItem, Task> OnHandleAdd { get; set; } = default!;

        [Parameter, EditorRequired]
        public HttpItem? Data { get; set; } = default!;

        public string NewHeaderKey { get; set; } = "";

        public FluentDialog Dialog = default!;

        private bool IsLoading = false;

        public async Task HandleOnSave()
        {
            IsLoading = true;
            
            await OnHandleAdd.Invoke(Data);

            Data = new();
            IsLoading = false;
            CloseDialog();
            base.StateHasChanged();
        }

        private async Task HandleOnHeaderAdd(FocusEventArgs args)
        {
            if (string.IsNullOrEmpty(NewHeaderKey)) return;

            var headers = Data.Request.Header.Where(item => !string.IsNullOrEmpty(item.Key) || !string.IsNullOrEmpty(item.Value)).ToList();
            headers.Add(new HttpHeader() { Key = NewHeaderKey, Value = "" });
            Data.Request.Header = headers;

            NewHeaderKey = "";
            base.StateHasChanged();
        }

        public void DismissDialog(DialogEventArgs args)
        {
            if (args is not null && args.Reason is not null && args.Reason == "dismiss")
            {
                Dialog.Hide();
            }
        }

        public void CloseDialog()
        {
            Dialog.Hide();
        }

        public void OpenAddDialog()
        {
            //if (Collection == null)
            //{// TODO: 
            //    //Collection = JsonConvert.DeserializeObject<HttpCollection>(_Constants.Json);
            //}

            Dialog.Show();
        }

        public void OpenEditDialog(HttpItem item)
        {
            Data = item;
            //if (Collection == null)
            //{// TODO: 
            //    //Collection = JsonConvert.DeserializeObject<HttpCollection>(_Constants.Json);
            //}

            Dialog.Show();
        }
    }
}
