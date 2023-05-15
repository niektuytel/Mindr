using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.Personal;

namespace Mindr.Api.Services.CalendarEvents
{
    public interface IPersonalCalendarValidator
    {
        void ThrowOnInvalidUserId(string userId);
        void ThrowOnNullPersonalCredential(string userId, string calendarId, PersonalCredential? entity);
        void ThrowOnNullPersonalCalendar(string userId, string calendarId, PersonalCalendar? entity);
    }
}