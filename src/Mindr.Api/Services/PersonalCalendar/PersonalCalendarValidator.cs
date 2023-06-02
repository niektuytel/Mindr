using Microsoft.EntityFrameworkCore;
using Mindr.Api.Persistence;
using Mindr.Api.Exceptions;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.Personal;
using Mindr.Domain.Models.DTO.Calendar;
using Microsoft.Graph;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Google.Apis.Auth.OAuth2;

namespace Mindr.Api.Services.CalendarEvents
{
    public class PersonalCalendarValidator : IPersonalCalendarValidator
    {
        public PersonalCalendarValidator()
        { }

        public void ThrowOnInvalidUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new HttpException<string>(HttpStatusCode.BadRequest, $"Unknown {nameof(userId)}:'{userId}'");
            }
        }

        public void ThrowOnNullPersonalCalendar(string userId, string? calendarId, PersonalCalendar? entity)
        {
            if (entity != null)
            {
                return;
            }

            throw new HttpException<string>(HttpStatusCode.NotFound, $"Can't find calendar on User:{userId} (calendarId:{calendarId})");
        }

        public void ThrowOnNullPersonalCalendars(string userId, string? calendarId, IEnumerable<PersonalCalendar>? entities)
        {
            if (entities?.Any() == true)
            {
                return;
            }

            throw new HttpException<string>(HttpStatusCode.NotFound, $"Can't find calendars on User:{userId} (calendarId:{calendarId})");
        }

        public void ThrowOnNullPersonalCredential(string userId, string? calendarId, PersonalCredential? entity)
        {
            if (entity == null)
            {
                throw new HttpException<string>(HttpStatusCode.NotFound, $"Can't find calendar credential on User:{userId} (calendarId:{calendarId})");
            }
        }

    }
}