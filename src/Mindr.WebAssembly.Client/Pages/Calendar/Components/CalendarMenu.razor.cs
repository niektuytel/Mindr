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
using MudBlazor;
using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.Models.DTO.Personal;
using Mindr.WebAssembly.Client.Models;
using System.Net;
using System.Globalization;

namespace Mindr.WebAssembly.Client.Pages.Calendar.Components
{
    public partial class CalendarMenu
    {
        [Parameter, EditorRequired]
        public string ViewType { get; set; } = default!;

        [Inject]
        public IApiPersonalCalendarClient CalendarClient { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        public CalendarDrawer? Drawer { get; set; } = default!;



        bool open = false;

        public bool IsLoading { get; set; } = true;

        public IEnumerable<PersonalCalendar>? Calendars { get; set; } = null;


        private string _selectedCalendar = "All";

        [Parameter]
        public string SelectedCalendar
        {
            get => _selectedCalendar;
            set
            {
                if (_selectedCalendar != value)
                {
                    _selectedCalendar = value;
                    SelectedCalendarChanged(_selectedCalendar);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            var response = await CalendarClient.GetCalendars();
            if(response.IsSuccessful())
            {
                Calendars = response.GetContent<IEnumerable<PersonalCalendar>>();
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

        private void ViewChanged(string path)
        {
            open = false;
            NavigationManager.NavigateTo(path);
        }

        public async Task ToggleDrawer()
        {
            open = !open;
        }

        private async void SelectedCalendarChanged(string calendarId)
        {
            await LocalStorage.SetItemAsync($"CalendarId", calendarId);
            NavigationManager.NavigateTo($"/calendar/{ViewType}/{calendarId}");
            open = false;
        }


        private async Task AddCalendar(PersonalCalendar calendar)
        {
            Calendars ??= new List<PersonalCalendar>();

            var alreadyExists = Calendars.Any(c => c.CalendarId == calendar.CalendarId);
            if(alreadyExists)
            {
                Snackbar.Add($"Calendar {calendar.Summary} already exists", Severity.Error);
                return;
            }

            Calendars = Calendars.Append(calendar);
            SelectedCalendar = calendar.Summary;
        }


    }
}