using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Mindr.WebUI.Services.ApiClients;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Mindr.WebUI.Views.Connectors.Components
{
    public partial class ConnectorDialog : FluentComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; }

        public FluentDialog Dialog = default!;

        public Connector Data { get; set; } = new();

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
