using Azure.Core;
using Google.Apis.Calendar.v3.Data;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Graph.TermStore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;
using Mindr.Api.Exceptions;
using Mindr.Api.Persistence;
using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Personal;
using Newtonsoft.Json;
using System.Data.SqlTypes;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Mindr.Api.Services.CalendarEvents
{
    public class GoogleCalendarClient : IGoogleCalendarClient
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;
        private readonly HttpClient _httpClient;

        public GoogleCalendarClient(IHttpClientFactory httpClientFactory, ApplicationContext context, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(GoogleCalendarClient));
            _context = context;
            _configuration = configuration;
        }

        public async Task<string?> GetAccessToken(PersonalCredential credential)
        {
            var clientId = _configuration["Google:ClientId"];
            var clientSecret = _configuration["Google:ClientSecret"];
            var uri = $"https://oauth2.googleapis.com/token?client_id={clientId}&client_secret={clientSecret}&grant_type=refresh_token&refresh_token={credential.RefreshToken}";

            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(string.Empty);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<JsonObject>(jsonString);
                var accessToken = jsonObject!["access_token"]!.GetValue<string>();

                return accessToken;
            }

            throw new HttpException<PersonalCredential>(System.Net.HttpStatusCode.NotFound, credential);
        }

        public async Task<IEnumerable<PersonalCalendar>?> GetCalendars(PersonalCredential credential, string userId)
        {
            var accessToken = await GetAccessToken(credential);
            var uri = $"https://www.googleapis.com/calendar/v3/users/me/calendarList";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("Accept", "application/json");
            var response = await _httpClient.SendAsync(request);

            var calendars = new List<PersonalCalendar>();
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var calendarList = JsonConvert.DeserializeObject<CalendarList>(jsonString);

                if (calendarList?.Items?.Any() == false)
                {
                    return null;
                }

                foreach (var item in calendarList.Items)
                {
                    if (item == null) continue;
                    if (string.IsNullOrEmpty(item.Id)) continue;

                    var calendarItem = new PersonalCalendar()
                    {
                        UserId = userId,
                        CredentialId = credential.Id,
                        From = Domain.Enums.CalendarFrom.Google,
                        CalendarId = item.Id,
                        Description = item.Description,
                        Summary = item.Summary,
                        Selected = false,
                        Color = item.ColorId
                    };

                    calendars.Add(calendarItem);

                }

                return calendars;
            }

            throw new Exception($"Failed getting calendars from credentials({credential.Id}) [Code:{response.StatusCode}]");
        }


        public HashSet<Occurrence> GetNextEvents(DateTime start, DateTime end, DateTime batchStart, DateTime batchEnd, string rrule)
        {
            var calendarEvent = new CalendarEvent
            {
                Start = new CalDateTime(start),
                End = new CalDateTime(end),
                RecurrenceRules = new List<Ical.Net.DataTypes.RecurrencePattern> { new Ical.Net.DataTypes.RecurrencePattern(rrule) }
            };

            return calendarEvent.GetOccurrences(new CalDateTime(batchStart), new CalDateTime(batchEnd));
        }

        public async Task<IEnumerable<CalendarAppointment>> GetCalendarAppointment(PersonalCredential personalCredential, string calendarId, DateTime startDateTime, DateTime endDateTime)
        {

            var accessToken = await GetAccessToken(personalCredential);
            var timespan = $"timeMin={startDateTime.Year}-{startDateTime.Month}-{startDateTime.Day}T00%3A00%3A00-00%3A00&timeMax={endDateTime.Year}-{endDateTime.Month}-{endDateTime.Day}T23%3A59%3A59-00%3A00";
            var uri = $"https://www.googleapis.com/calendar/v3/calendars/{calendarId}/events?{timespan}";


            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Headers.Add("Accept", "application/json");
            var response = await _httpClient.SendAsync(request);

            var appointments = new List<CalendarAppointment>();
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var dataEvents = JsonConvert.DeserializeObject<Events>(jsonString);

                if (dataEvents?.Items?.Any() == false)
                {
                    return null;
                }

                foreach (var item in dataEvents.Items)
                {
                    if (item == null) continue;
                    if (string.IsNullOrEmpty(item.Id)) continue;
                    if (item.Status == "cancelled") continue;


                    var start = item.Start.DateTime;
                    if (start == null)
                    {
                        start = DateTime.Parse(item.Start.Date);
                    }

                    var end = item.End.DateTime;
                    if (end == null)
                    {
                        end = DateTime.Parse(item.End.Date);
                    }

                    var events = _context.ConnectorEvents.Where(i =>
                        i.UserId == personalCredential.UserId &&
                        i.EventId == item.Id
                    ).AsEnumerable();

                    // set recurrence date
                    if (item.Recurrence != null && item.Recurrence.Any())
                    {
                        try
                        {
                            // TODO: Oppassen Milou failed hier
                            var nextEvents = GetNextEvents((DateTime)start, (DateTime) end, startDateTime, endDateTime, item.Recurrence[0]);
                            foreach (var nextEvent in nextEvents)
                            {
                                var appointment = new CalendarAppointment(
                                    item.Id,
                                    calendarId,
                                    item.Summary,
                                    nextEvent.Period.StartTime.Value,
                                    item?.Start?.TimeZone,
                                    nextEvent.Period.EndTime.Value,
                                    item?.End?.TimeZone,
                                    events,
                                    item?.ColorId
                                );

                                appointments.Add(appointment);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed getting next events from calendar({calendarId}) ex:{ex.Message}");
                            continue;
                        }
                    }
                    else
                    {
                        // None recurrence date
                        var appointment = new CalendarAppointment(
                            item.Id,
                            calendarId,
                            item.Summary,
                            start,
                            item?.Start?.TimeZone,
                            end,
                            item?.End?.TimeZone,
                            events,
                            item?.ColorId
                        );
                        
                        appointments.Add(appointment);
                    }

                }

            }

            Console.Error.WriteLine($"Failed getting events from calendar({calendarId}) [Code:{response.StatusCode}]");
            return appointments;
        }

        public async Task<CalendarAppointment> InsertCalendarAppointment(PersonalCredential personalCredential, string calendarId, object appointmentId, CalendarAppointment input)
        {
            var accessToken = await GetAccessToken(personalCredential);
            // TODO: Insert appointment for google calendar

            //          curl --request POST \
            //'https://www.googleapis.com/calendar/v3/calendars/a38067012fae80d9b938b59ff0be170eed5d4dc0109d438bebb6273a83eb1301%40group.calendar.google.com/events?key=[YOUR_API_KEY]' \
            //--header 'Authorization: Bearer [YOUR_ACCESS_TOKEN]' \
            //--header 'Accept: application/json' \
            //--header 'Content-Type: application/json' \
            //--data '{"end":{"dateTime":"2023-06-19T23:04:00.324Z","timeZone":""},"start":{"dateTime":"2023-06-18T23:04:00.324Z"}}' \
            //--compressed

            // RESPONSE: {
            // "kind": "calendar#event",
            // "etag": "\"3373913399416000\"",
            // "id": "t01d9e26vdfpvvdq4n7a2v3e1s",
            // "status": "confirmed",
            // "htmlLink": "https://www.google.com/calendar/event?eid=dDAxZDllMjZ2ZGZwdnZkcTRuN2EydjNlMXMgYTM4MDY3MDEyZmFlODBkOWI5MzhiNTlmZjBiZTE3MGVlZDVkNGRjMDEwOWQ0MzhiZWJiNjI3M2E4M2ViMTMwMUBn",
            // "created": "2023-06-16T23:04:59.000Z",
            // "updated": "2023-06-16T23:04:59.708Z",
            // "creator": {
            //  "email": "tuytelniek@gmail.com"
            // },
            // "organizer": {
            //  "email": "a38067012fae80d9b938b59ff0be170eed5d4dc0109d438bebb6273a83eb1301@group.calendar.google.com",
            //  "displayName": "Agenda Niek & Angelique",
            //  "self": true
            // },
            // "start": {
            //  "dateTime": "2023-06-19T01:04:00+02:00",
            //  "timeZone": "Europe/Amsterdam"
            // },
            // "end": {
            //  "dateTime": "2023-06-20T01:04:00+02:00",
            //  "timeZone": "Europe/Amsterdam"
            // },
            // "iCalUID": "t01d9e26vdfpvvdq4n7a2v3e1s@google.com",
            // "sequence": 0,
            // "reminders": {
            //  "useDefault": true
            // },
            // "eventType": "default"
            //}


            return input;
        }

        public async Task<CalendarAppointment> UpdateCalendarAppointment(PersonalCredential personalCredential, string calendarId, string appointmentId, CalendarAppointment input)
        {
            var accessToken = await GetAccessToken(personalCredential);
            // TODO: Update appointment for google calendar

            //          curl--request PUT \
            //'https://www.googleapis.com/calendar/v3/calendars/a38067012fae80d9b938b59ff0be170eed5d4dc0109d438bebb6273a83eb1301%40group.calendar.google.com/events/t01d9e26vdfpvvdq4n7a2v3e1s?key=[YOUR_API_KEY]' \
            //--header 'Authorization: Bearer [YOUR_ACCESS_TOKEN]' \
            //--header 'Accept: application/json' \
            //--header 'Content-Type: application/json' \
            //--data '{"end":{"dateTime":"2023-06-19T01:04:00+02:00"},"start":{"dateTime":"2023-06-12T01:04:00+02:00"}}' \
            //--compressed

            // RESPONSE: {
            // "kind": "calendar#event",
            // "etag": "\"3373915061220000\"",
            // "id": "t01d9e26vdfpvvdq4n7a2v3e1s",
            // "status": "confirmed",
            // "htmlLink": "https://www.google.com/calendar/event?eid=dDAxZDllMjZ2ZGZwdnZkcTRuN2EydjNlMXMgYTM4MDY3MDEyZmFlODBkOWI5MzhiNTlmZjBiZTE3MGVlZDVkNGRjMDEwOWQ0MzhiZWJiNjI3M2E4M2ViMTMwMUBn",
            // "created": "2023-06-16T23:04:59.000Z",
            // "updated": "2023-06-16T23:18:50.610Z",
            // "creator": {
            //                "email": "tuytelniek@gmail.com"
            // },
            // "organizer": {
            //                "email": "a38067012fae80d9b938b59ff0be170eed5d4dc0109d438bebb6273a83eb1301@group.calendar.google.com",
            //  "displayName": "Agenda Niek & Angelique",
            //  "self": true
            // },
            // "start": {
            //                "dateTime": "2023-06-12T01:04:00+02:00",
            //  "timeZone": "Europe/Amsterdam"
            // },
            // "end": {
            //                "dateTime": "2023-06-19T01:04:00+02:00",
            //  "timeZone": "Europe/Amsterdam"
            // },
            // "iCalUID": "t01d9e26vdfpvvdq4n7a2v3e1s@google.com",
            // "sequence": 1,
            // "reminders": {
            //                "useDefault": true
            // },
            // "eventType": "default"
            //}
            return input;
        }

        public async Task<CalendarAppointment> DeleteCalendarAppointment(PersonalCredential personalCredential, string calendarId, string appointmentId)
        {
            var accessToken = await GetAccessToken(personalCredential);
            // TODO: Delete appointment for google calendar

            //          curl --request DELETE \
            //'https://www.googleapis.com/calendar/v3/calendars/a38067012fae80d9b938b59ff0be170eed5d4dc0109d438bebb6273a83eb1301%40group.calendar.google.com/events/t01d9e26vdfpvvdq4n7a2v3e1s?key=[YOUR_API_KEY]' \
            //--header 'Authorization: Bearer [YOUR_ACCESS_TOKEN]' \
            //--header 'Accept: application/json' \
            //--compressed

            // RESPONSE : 204
            return new CalendarAppointment() { CalendarId = calendarId, Id = appointmentId };
        }
    }
}
