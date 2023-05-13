using Microsoft.EntityFrameworkCore;
using Mindr.Api.Persistence;
using Mindr.Api.Exceptions;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.PersonalCredential;
using Mindr.Domain.Models.DTO.CalendarEvent;

namespace Mindr.Api.Services.CalendarEvents
{
    public class CalendarEventValidator : ICalendarEventValidator
    {
        public CalendarEventValidator()
        { }

        public void ThrowOnInvalidUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new HttpException(HttpStatusCode.BadRequest, $"Unknown {nameof(userId)}:'{userId}'");
            }
        }

        public void ThrowOnNullCalendarEvent(Guid? id, CalendarEvent? entity)
        {
            if (id == null)
            {
                throw new HttpException(HttpStatusCode.BadRequest, $"Connector id '{id}' is null");
            }

            if (entity == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"Can't find connector on {nameof(id)}:'{id}' that is public");
            }
        }

    }
}