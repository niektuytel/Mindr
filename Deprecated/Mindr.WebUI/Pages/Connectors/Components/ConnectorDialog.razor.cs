using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Graph.TermStore;
using Mindr.Core.Models.ConnectorEvents;
using Mindr.Core.Models.Connectors;
using Mindr.Client.Services;
using Newtonsoft.Json;

namespace Mindr.Client.Pages.Connectors.Components
{
    public partial class ConnectorDialog : FluentComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; }

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
            IsLoadingData = false;

            if (response == null)
            {
                // TODO: should be fixed with refresh token

                ErrorMessage = $"Login session expired, Please login again";
                base.StateHasChanged();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<Connector>(content);
                    NavigationManager.NavigateTo($"/connectors/{data!.Id}/pipeline");
                    HandleDialogClose();
                }
                else
                {
                    ErrorMessage = content;
                    base.StateHasChanged();
                }
            }

            //HandleDialogClose();
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
