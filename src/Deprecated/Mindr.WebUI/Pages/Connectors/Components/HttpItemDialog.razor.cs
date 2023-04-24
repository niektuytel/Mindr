using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Fast.Components.FluentUI;
using Mindr.HttpRunner.Models;

namespace Mindr.Client.Pages.Connectors.Components
{
    // Usefull to use? https://www.postman.com/cs-demo/workspace/public-rest-apis/overview
    public partial class HttpItemDialog : FluentComponentBase
    {
        [Parameter, EditorRequired]
        public Func<HttpItem, Task> OnCreate { get; set; } = default!;

        [Parameter, EditorRequired]
        public Func<HttpItem, Task> OnUpdate { get; set; } = default!;

        [Parameter, EditorRequired]
        public HttpItem? Data { get; set; } = default!;

        public string NewHeaderKey { get; set; } = "";

        public string ButtonText { get; set; } = "";

        public FluentDialog Dialog = default!;

        private bool IsLoading = false;

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Dialog.Hide();
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        public async Task HandleOnSave()
        {
            IsLoading = true;

            if (ButtonText == "Create")
            {
                await OnCreate.Invoke(Data);

            }
            else if (ButtonText == "Update")
            {
                await OnUpdate.Invoke(Data);

            }
            else
            {
                throw new NotImplementedException($"{ButtonText} does not exist");
            }


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
            Data = new();
            ButtonText = "Create";
            //if (Collection == null)
            //{// TODO: 
            //    //Collection = JsonConvert.DeserializeObject<HttpCollection>(_Constants.Json);
            //}

            Dialog.Show();
        }

        public void OpenEditDialog(HttpItem item)
        {
            Data = item;
            ButtonText = "Update";
            //if (Collection == null)
            //{// TODO: 
            //    //Collection = JsonConvert.DeserializeObject<HttpCollection>(_Constants.Json);
            //}

            Dialog.Show();
        }
    }
}
