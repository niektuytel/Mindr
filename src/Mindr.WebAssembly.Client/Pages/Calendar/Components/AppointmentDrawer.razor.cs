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
using BlazorScheduler;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.WebAssembly.Client.Pages.Calendar.Dialogs;

namespace Mindr.WebAssembly.Client.Pages.Calendar.Components
{
    public partial class AppointmentDrawer
    {
        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        public CalendarAppointment Appointment { get; set; } = new();

        DateRange __dateRange;
        DateRange _dateRange
        {
            get
            {
                return __dateRange;
            }

            set
            {
                (Appointment.StartDate.DateTime, Appointment.EndDate.DateTime) = (value.Start ?? Appointment.StartDate.DateTime, value.End ?? Appointment.EndDate.DateTime);
                (__dateRange.Start, __dateRange.End) = (value.Start, value.End);
            }
        }

        private bool DataHasChanged = false;

        private bool Open = false;

        private bool success;

        void OnConnectorEventRemove(ConnectorEvent connectorEvent)
        {
            Appointment.ConnectorEvents = Appointment.ConnectorEvents.Where(item => item.Id != connectorEvent.Id);
        }

        void OnConnectorEventUpsert(DialogResult result, bool isCreate)
        {
            if (result.Canceled == true) return;

            var connectorEvent = result.Data as ConnectorEvent;
            if (isCreate)
            {
                // Insert
                var events = Appointment.ConnectorEvents.ToList();
                events.Add(connectorEvent);

                Appointment.ConnectorEvents = events.AsEnumerable();
            }
            else
            {
                // Update
                Appointment.ConnectorEvents = Appointment.ConnectorEvents.Select(item => item.Id == connectorEvent!.Id ? connectorEvent : item);
            }

            DataHasChanged = true;
            base.StateHasChanged();
        }   

        public async Task OnConnectorEventClicked(ConnectorEvent? connectorEvent = null)
        {
            var isCreate = connectorEvent == null;
            var action = isCreate ? "Create" : "Edit";
            var parameters = new DialogParameters
            {
                ["Item"] = connectorEvent ?? new ConnectorEvent(),
                ["Label"] = $"{action} connector",
            };
            var maxWidth = new DialogOptions() 
            { 
                MaxWidth = MaxWidth.Medium, 
                FullWidth = true 
            };

            var dialog = DialogService.Show<ConnectorEventDialog>($"{action} Connector Event", parameters, maxWidth);
            var result = await dialog.Result;

            OnConnectorEventUpsert(result, isCreate);
        }

        public Task OnOpen(CalendarAppointment appointment)
        {
            Appointment = appointment;
            __dateRange = new DateRange(Appointment.StartDate?.DateTime, Appointment.EndDate?.DateTime);

            Open = true;
            base.StateHasChanged();
            return Task.CompletedTask;
        }

        public void OnConfirm()
        {
            // TODO: Upsert the appointment (with loading on this button)

            Open = false;
            base.StateHasChanged();
        }

        public void OnCancel()
        {
            Open = false;
            base.StateHasChanged();
        }

    }
}