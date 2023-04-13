using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Extensions;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Mindr.Core.Services.Connectors;
using Newtonsoft.Json;

namespace Mindr.WebUI.Pages.Connectors.Components
{
    public partial class ConfirmDialog: FluentComponentBase
    {
        [Parameter, EditorRequired]
        public string Text { get; set; } = default!;

        [Parameter, EditorRequired]
        public Func<Task> OnConfirm { get; set; } = default!;

        public FluentDialog Dialog = default!;

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Dialog.Hide();
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //}

        public void OnHandleClose()
        {
            Dialog.Hide();
        }

        public void OpenDialog()
        {
            Dialog.Show();
        }

        public void OnHandleDismiss(DialogEventArgs args)
        {
            if (args is not null && args.Reason is not null && args.Reason == "dismiss")
            {
                Dialog.Hide();
            }
        }

    }
}
