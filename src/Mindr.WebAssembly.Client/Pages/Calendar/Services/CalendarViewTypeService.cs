using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using System.Web;

namespace Mindr.WebAssembly.Client.Pages.Calendar.Services
{
    public class CalendarViewTypeService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;

        public event Action OnChange;

        public string CookieKey => nameof(CalendarViewTypeService);

        public string Value { get; private set; } = "month";

        public CalendarViewTypeService(ILocalStorageService localStorage, NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        public async Task InitializeAsync()
        {
            var uri = new Uri(_navigationManager.Uri);
            var calendarViewType = HttpUtility.ParseQueryString(uri.Query).Get("calendarViewType");

            // Convert calendarId and calendarViewType to their respective types if necessary
            // then use these values in your component as needed

            if (await TrySetValue(calendarViewType))
            {
                return;
            }

            var exists = await _localStorage.ContainKeyAsync(CookieKey);
            if (exists)
            {
                Value = await _localStorage.GetItemAsync<string>(CookieKey);
            }
        }

        private async Task<bool> TrySetValue(string? calendarViewType)
        {
            if (string.IsNullOrEmpty(calendarViewType))
            {
                return false;
            }

            Value = calendarViewType;

            // set cookie
            await _localStorage.SetItemAsync(CookieKey, calendarViewType);

            return true;
        }

        public async Task SetOnAction(string? calendarViewtype)
        {
            if (await TrySetValue(calendarViewtype) == false)
            {
                return;
            }

            OnChange?.Invoke();
        }

        internal bool IsDay()
        {
            return Value.ToLower() == "day";
        }

        internal bool IsWeek()
        {
            return Value.ToLower() == "week";
        }

        internal bool IsMonth()
        {
            return string.IsNullOrEmpty(Value) || Value.ToLower() == "month";
        }
    }
}
