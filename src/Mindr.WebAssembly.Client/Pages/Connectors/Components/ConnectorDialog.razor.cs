using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Shared.Models.Connectors;
using Mindr.Client.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Mindr.Client.Pages.Connectors.Components
{
    public partial class ConnectorDialog : FluentComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IApiConnectorClient ConnectorClient { get; set; }

        public Connector Data { get; set; } = new();

        private string? ErrorMessage { get; set; }

        public FluentDialog Dialog = default!;
        private bool IsLoadingData = false;
        private bool IsLoadingDialog = false;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Dialog.Hide();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task OnConnectorAdd()
        {
            IsLoadingData = true;

            var response = await ConnectorClient.Create(Data);
            (var data, ErrorMessage) = response.AsTuple();
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                base.StateHasChanged();
            }
            else if (data != null)
            {
                NavigationManager.NavigateTo($"/connectors/{data!.Id}/pipeline");
                HandleDialogClose();
            }

            IsLoadingData = false;
            base.StateHasChanged();
        }

        public void HandleDialogOpen()
        {

            Dialog.Show();
        }

        public void HandleDialogClose()
        {
            Dialog.Hide();
        }

        public void HandleDialogDismiss(DialogEventArgs args)
        {
            if (args is not null && args.Reason is not null && args.Reason == "dismiss")
            {
                Dialog.Hide();
            }
        }
    }
}
