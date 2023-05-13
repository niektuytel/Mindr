using Mindr.Domain.Models.DTO.CalendarEvent;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.PersonalCredential;

namespace Mindr.Api.Services.CalendarEvents
{
    public interface ICalendarEventValidator
    {
        void ThrowOnInvalidUserId(string userId);
        void ThrowOnNullCalendarEvent(Guid? id, CalendarEvent? entity);
    }
}