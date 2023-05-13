using Azure.Core;
using Microsoft.Graph;
using Mindr.Domain.Models.DTO.CalendarEvent;
using Mindr.Domain.Models.GoogleCalendar;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Mindr.Api.Services.CalendarEvents
{
    public class GoogleCalendarClient : IGoogleCalendarClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public GoogleCalendarClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(GoogleCalendarClient));
            _configuration = configuration;
        }

        public async Task<string> GetAccessToken(string refreshToken)
        {
            var clientId = _configuration["Google:ClientId"];
            var clientSecret = _configuration["Google:ClientSecret"];
            var uri = $"https://oauth2.googleapis.com/token?client_id={clientId}&client_secret={clientSecret}&grant_type=refresh_token&refresh_token={refreshToken}";

            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(string.Empty);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonSerializer.Deserialize<JsonObject>(jsonString);
                var accessToken = jsonObject!["access_token"]!.GetValue<string>();

                return accessToken;
            }

            throw new Exception($"Failed getting access token [Code:{response.StatusCode}]");
        }

        public async Task<GoogleCalendarEvents?> GetCalendarEvents(Task<string> accessToken, string calendarId, DateTime startDateTime, DateTime endDateTime)
        {
            var timespan = $"timeMin={startDateTime.Year}-{startDateTime.Month}-{startDateTime.Day}T00%3A00%3A00-00%3A00&timeMax={endDateTime.Year}-{endDateTime.Month}-{endDateTime.Day}T23%3A59%3A59-00%3A00";
            var uri = $"https://www.googleapis.com/calendar/v3/calendars/{calendarId}/events?{timespan}";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("Accept", "application/json");
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var events = JsonSerializer.Deserialize<GoogleCalendarEvents>(jsonString);
                return events;
            }

            throw new Exception($"Failed getting events from calendar({calendarId}) [Code:{response.StatusCode}]");
        }


    }
}
