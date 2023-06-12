using Blazored.LocalStorage;
using DemoApp.Models;
using Microsoft.AspNetCore.Components;
using Mindr.Domain.Models.DTO.Calendar;
using Mindr.WebAssembly.Client.Pages.Calendar.Components;
using Mindr.WebAssembly.Client.Pages.Calendar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApp
{
    public class CalendarAppointmentsService
    {
        private readonly ILocalStorageService _localStorage;

        public event Action OnChange;

        public string CookieKey => nameof(CalendarAppointmentsService);

        public IEnumerable<CalendarAppointment> Values { get; private set; } = new List<CalendarAppointment>();

        public CalendarAppointmentsService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task InitializeAsync()
        {
        }

        private async Task<bool> TrySetValue(IEnumerable<CalendarAppointment> appointments)
        {
            Values = appointments;
            return true;
        }

        public async Task SetOnAction(IEnumerable<CalendarAppointment> appointments)
        {
            if (await TrySetValue(appointments) == false)
            {
                return;
            }

            OnChange?.Invoke();
        }

        private IEnumerable<Appointment> GetInDateRange(DateTime start, DateTime end)
        {
            var appointmentsInTimeframe = Values
                .Where(x => x.IsVisible)
                .Where(x => (start, end).Overlaps((x.Start.Date, x.End.Date)));

            return appointmentsInTimeframe
                .OrderBy(x => x.Start)
                .ThenByDescending(x => (x.End - x.Start).Days);
        }

    }
}
