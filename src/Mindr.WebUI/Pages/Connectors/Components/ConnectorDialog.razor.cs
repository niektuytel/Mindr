using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Graph.TermStore;
using Mindr.Core.Models.Connectors;
using Mindr.WebUI.Services;
using Newtonsoft.Json;

namespace Mindr.WebUI.Pages.Connectors.Components
{
    public partial class ConnectorDialog : FluentComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; }

        private string ErrorMessage { get; set; } = "Test error message exzplain what is going wrong from api";

        public Connector Data { get; set; } = new();

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
            if (response == null)
            {
                // Failed request
                throw new NotImplementedException();
            }

            var json = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(json))
            {
                var data = JsonConvert.DeserializeObject<Connector>(json);
                NavigationManager.NavigateTo($"/connectors/{data!.Id}/pipeline");
            }
            else
            {
                // TODO: create error message
            }

            //IsLoading = false;
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
