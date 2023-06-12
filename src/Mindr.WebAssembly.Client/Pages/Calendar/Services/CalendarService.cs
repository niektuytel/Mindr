using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Web;

namespace Mindr.WebAssembly.Client.Pages.Calendar.Services
{
    public class CalendarService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;

        public event Action OnChange;

        public string CookieKey => nameof(CalendarService);

        public string Value { get; private set; } = "All";

        public CalendarService(ILocalStorageService localStorage, NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        public async Task InitializeAsync()
        {
            var uri = new Uri(_navigationManager.Uri);
            var calendarId = HttpUtility.ParseQueryString(uri.Query).Get("calendarId");
            if (await TrySetValue(calendarId))
            {
                return;
            }

            var exists = await _localStorage.ContainKeyAsync(CookieKey);
            if (exists)
            {
                Value = await _localStorage.GetItemAsync<string>(CookieKey);
            }
        }

        private async Task<bool> TrySetValue(string? calendarId)
        {
            if (string.IsNullOrEmpty(calendarId))
            {
                return false;
            }

            Value = calendarId;

            // Set cookie
            await _localStorage.SetItemAsync(CookieKey, calendarId);

            return true;
        }

        public async Task SetOnAction(string? calendarId)
        {
            if (await TrySetValue(calendarId) == false)
            {
                return;
            }

            OnChange?.Invoke();
        }

    }
}
