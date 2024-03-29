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

namespace Mindr.WebAssembly.Client.Pages.Calendar.Components
{
    public partial class AppointmentDrawer
    {
        [Inject]
        public IApiPersonalCalendarClient CalendarClient { get; set; } = default!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        [Inject]
        public CalendarService CalendarService { get; set; } = default!;

        public CalendarAppointment Appointment { get; set; } = new();

        public bool IsLoading = false;

        DateRange _dateRange;

        private bool DataHasChanged = false;
        
        public bool IsInsert = false;

        private bool Open = false;

        private bool success;


        void OnConnectorEventRemove(ConnectorEvent connectorEvent)
        {
            Appointment.ConnectorEvents = Appointment.ConnectorEvents.Where(item => item.Id != connectorEvent.Id);

            DataHasChanged = true;
            base.StateHasChanged();
        }

        void OnConnectorEventUpsert(DialogResult result, bool isCreate)
        {
            if (result.Canceled == true) return;

            var connectorEvent = result.Data as ConnectorEvent;
            connectorEvent!.EventId = Appointment.Id;

            if (isCreate)
            {
                // Insert
                var events = Appointment.ConnectorEvents.ToList();
                events.Add(connectorEvent);

                Appointment.ConnectorEvents = events.AsEnumerable();
            }
            else
            {
                Appointment.ConnectorEvents = Appointment.ConnectorEvents.Select(item => item.Id == connectorEvent!.Id ? connectorEvent : item);
            }

            DataHasChanged = true;
            base.StateHasChanged();
        }   

        public async Task OnConnectorEventClicked(ConnectorEvent? connectorEvent = null)
        {
            var isCreate = connectorEvent == null;
            var parameters = new DialogParameters
            {
                ["Item"] = connectorEvent ?? new ConnectorEvent()
            };
            var options = new DialogOptions() 
            { 
                MaxWidth = MaxWidth.Medium, 
                FullWidth = true,
                CloseOnEscapeKey = true,
                NoHeader = true
            };

            var dialog = DialogService.Show<ConnectorEventDialog>("", parameters, options);
            var result = await dialog.Result;

            OnConnectorEventUpsert(result, isCreate);
        }

        public Task OnOpen(CalendarAppointment appointment, bool isInsert = false)
        {
            IsInsert = isInsert;
            Appointment = appointment;
            _dateRange = new DateRange(Appointment.StartDate.GetDateTime(), Appointment.EndDate.GetDateTime());

            DataHasChanged = false;
            Open = true;
            base.StateHasChanged();
            return Task.CompletedTask;
        }

        public async Task OnConfirm()
        {
            // set calendar id
            Appointment.CalendarId = CalendarService.Value;

            IsLoading = true;
            if (IsInsert)
            {
                var response = await CalendarClient.InsertAppointment(Appointment.CalendarId, Appointment);
                if (response.IsError())
                {
                    var error = response.GetContent();
                    Snackbar.Add(error, Severity.Error);
                }
                else if (response.IsSuccessful())
                {
                    Appointment = response.GetContent<CalendarAppointment>();
                }
            }
            else
            {
                // Update
                var response = await CalendarClient.UpdateAppointment(Appointment.CalendarId, Appointment);
                if (response.IsError())
                {
                    var error = response.GetContent();
                    Snackbar.Add(error, Severity.Error);
                }
                else if (response.IsSuccessful())
                {
                    Appointment = response.GetContent<CalendarAppointment>();
                }
            }

            IsLoading = false;
            Open = false;
            base.StateHasChanged();
        }

        public void OnCancel()
        {
            Open = false;
            base.StateHasChanged();
        }

        public async Task OnDelete()
        {
            // set calendar id
            Appointment.CalendarId = CalendarService.Value;

            IsLoading = true;
            var response = await CalendarClient.DeleteAppointment(Appointment.CalendarId, Appointment);
            if (response.IsError())
            {
                var error = response.GetContent();
                Snackbar.Add(error, Severity.Error);
            }
            else if (response.IsSuccessful())
            {
                Appointment = response.GetContent<CalendarAppointment>();
            }

            IsLoading = false;
            Open = false;
            base.StateHasChanged();
        }

    }
}