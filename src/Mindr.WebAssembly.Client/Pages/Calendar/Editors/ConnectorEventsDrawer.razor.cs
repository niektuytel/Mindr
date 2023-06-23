using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using Microsoft.Extensions.Logging;
using DemoApp.Models;
using Mindr.Domain.Models.DTO.Calendar;
using MudBlazor;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.WebAssembly.Client.Pages.Connectors.Components;
using Mindr.WebAssembly.Client.Pages.Calendar.Components;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.HttpRunner.Models;
using Mindr.WebAssembly.Client.Pages.Calendar.Services;
using Mindr.Domain.Models.DTO.Personal;
using System.Globalization;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Pages.Calendar.Components
{
    public partial class ConnectorEventsDrawer
    {
        [Inject]
        public IApiPersonalCalendarClient CalendarClient { get; set; } = default!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        [Inject]
        public CalendarService CalendarService { get; set; } = default!;

        public List<ConnectorEvent> ConnectorEvents { get; set; } = new();

        public ConnectorEvent? SelectedItem { get; set; } = null;

        public bool IsLoading = false;
        
        public bool IsInsert = false;

        private bool Open = false;

        public async Task OnOpen()
        {
            await GetAllConnectorEvents();

            Open = true;
            base.StateHasChanged();
        }

        async Task OpenConnectorEventDialog(ConnectorEvent? connectorEvent)
        {
            var isCreate = connectorEvent == null;

            connectorEvent ??= new ConnectorEvent();
            var parameters = new DialogParameters{ ["Item"] = connectorEvent };
            var options = new DialogOptions()
            {
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseOnEscapeKey = true,
                NoHeader = true
            };

            var dialog = DialogService.Show<ConnectorEventDialog>("", parameters, options);
            var result = await dialog.Result;

            await UpsertConnectorEvent(result, isCreate);
        }

        private async Task GetAllConnectorEvents()
        {
            IsLoading = true;
            var response = await CalendarClient.GetConnectorEvents(CalendarService.Value);
            if (response.IsSuccessful())
            {
                ConnectorEvents = response.GetContent<List<ConnectorEvent>>();
            }
            else
            {
                var errorMessage = response.GetContent<ErrorMessageResponse>();
                Snackbar.Add(errorMessage.Content, Severity.Error);
                Console.Error.WriteLine(errorMessage.Content);
            }

            IsLoading = false;
            base.StateHasChanged();
        }

        private async Task RemoveConnectorEvent()
        {
            if (SelectedItem == null) return;
            IsLoading = true;

            var response = await CalendarClient.DeleteConnectorEvent(SelectedItem.Id);
            if (response.IsError())
            {
                var error = response.GetContent();
                Snackbar.Add(error, Severity.Error);
            }
            else if (response.IsSuccessful())
            {
                var deletedItem = response.GetContent<ConnectorEvent>();
                var succeeded = ConnectorEvents.Remove(deletedItem);
                if(succeeded)
                {
                    SelectedItem = null;
                }
            }

            IsLoading = false;
            base.StateHasChanged();
        }

        private async Task UpsertConnectorEvent(DialogResult result, bool onInsert = true)
        {
            if (SelectedItem == null) return;
            if (result.Canceled == true) return;

            var connectorEvent = result.Data as ConnectorEvent;
            if(connectorEvent == null)
            {
                Snackbar.Add($"Invalid data, onInsert:{onInsert}", Severity.Error);
                return;
            }

            IsLoading = true;
            if (onInsert)
            {
                var response = await CalendarClient.InsertConnectorEvent(CalendarService.Value, connectorEvent);
                if (response.IsError())
                {
                    var error = response.GetContent();
                    Snackbar.Add(error, Severity.Error);
                }
                else if (response.IsSuccessful())
                {
                    var createdItem = response.GetContent<ConnectorEvent>();
                    ConnectorEvents.Add(createdItem);
                }
            }
            else
            {
                SelectedItem!.Update(connectorEvent);
                var response = await CalendarClient.UpdateConnectorEvent(SelectedItem);
                if (response.IsError())
                {
                    var error = response.GetContent();
                    Snackbar.Add(error, Severity.Error);
                }
                else if (response.IsSuccessful())
                {
                    var updatedItem = response.GetContent<ConnectorEvent>();
                    ConnectorEvents = ConnectorEvents
                        .Select(item => item.Id == updatedItem!.Id ? updatedItem : item)
                        .ToList();
                }
            }

            IsLoading = false;
            base.StateHasChanged();
        }

        private async Task SelectConnectorEvent(ConnectorEvent connectorEvent)
        {
            SelectedItem = connectorEvent;
            base.StateHasChanged();
        }

        public void OnCancel()
        {
            Open = false;
            base.StateHasChanged();
        }

    }
}