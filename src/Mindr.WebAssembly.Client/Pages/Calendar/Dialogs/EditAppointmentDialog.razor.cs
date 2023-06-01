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

namespace Mindr.WebAssembly.Client.Pages.Calendar.Dialogs
{
    public partial class EditAppointmentDialog
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public CalendarAppointment Appointment { get; set; }

        private AppointmentDrawer? ItemDrawer = default!;

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

        protected override void OnParametersSet()
        {
            __dateRange = new DateRange(Appointment.StartDate?.DateTime, Appointment.EndDate?.DateTime);
            base.OnParametersSet();
        }


        void Close() => MudDialog.Close(DialogResult.Ok(true));

        async Task OnHandleCreateConnectorEvent(ConnectorEvent connectorEvent)
        {
            Appointment.ConnectorEvents = Appointment.ConnectorEvents.Append(connectorEvent);
        }

        async Task OnHandleUpdateConnectorEvent(ConnectorEvent connectorEvent)
        {
            Appointment.ConnectorEvents = Appointment.ConnectorEvents.Select(item => item.Id == connectorEvent.Id ? connectorEvent : item);
        }
        
        void OnHandleRemoveConnectorEvent(ConnectorEvent connectorEvent)
        {
            Appointment.ConnectorEvents = Appointment.ConnectorEvents.Where(item => item.Id != connectorEvent.Id);
        }

    }
}