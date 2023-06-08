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
using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Personal;
using System.Globalization;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Pages.Calendar.Components
{
    public partial class CalendarDrawer
    {
        [Inject]
        public IApiPersonalCalendarClient CalendarClient { get; set; } = default!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Parameter, EditorRequired]
        public Func<PersonalCalendar, Task> AddCalendarAction { get; set; } = default!;

        public GoogleAuthentication? GoogleAuthentication { get; set; } = default!;

        public PersonalCalendar Calendar { get; set; } = new();

        public IEnumerable<PersonalCalendar>? Calendars { get; set; } = null;

        public PersonalCalendar? SelectedCalendar { get; set; } = null;

        public string RedirectUri => $"{NavigationManager.BaseUri[..^1]}/calendar";

        private bool DataHasChanged = false;
        public bool IsLoading = false;
        private bool Open = false;
        private bool success;
        private bool isInsert;

        //protected override async Task OnInitializedAsync()
        //{
        //    //await LoadExternalCalendars();
        //    await base.OnInitializedAsync();
        //}

        public async Task OnOpen(PersonalCalendar? calendar = null)
        {
            isInsert = calendar == null;
            Calendar = calendar ?? new PersonalCalendar();

            if(Calendars?.Any() != true)
            {
                await LoadExternalCalendars();

                if (Calendars?.Any() != true)
                { 
                    await GoogleAuthentication!.HandleConsent();
                }
            }

            Open = true;
            base.StateHasChanged();
        }

        public async Task LoadExternalCalendars()
        {
            IsLoading = true;
            var response = await CalendarClient.GetExternalCalendars();
            if (response.IsSuccessful())
            {
                Calendars = response.GetContent<IEnumerable<PersonalCalendar>>();
                Open = true;
                base.StateHasChanged();
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

        public async Task OnConfirm()
        {
            if (SelectedCalendar == null)
            {
                Snackbar.Add("Please select a calendar", Severity.Error);
                return;
            }

            IsLoading = true;
            if (isInsert)
            {
                var response = await CalendarClient.InsertCalendar(SelectedCalendar);
                if (response.IsError())
                {
                    var errorMessage = response.GetContent<ErrorMessageResponse>();
                    Snackbar.Add(errorMessage.Content, Severity.Error);
                    Console.Error.WriteLine(errorMessage.Content);
                }
                else if (response.IsSuccessful())
                {
                    Calendar = response.GetContent<PersonalCalendar>();
                    await AddCalendarAction(Calendar);
                }
            }
            else
            {
                // Update
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

    }
}