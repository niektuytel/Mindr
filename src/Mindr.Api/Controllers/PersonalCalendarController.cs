using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mindr.Api.Extensions;
using Mindr.Api.Models;
using Mindr.Api.Services.CalendarEvents;
using Mindr.Domain.Enums;

using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Personal;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mindr.Api.Controllers;

[Authorize]
public class PersonalCalendarController : BaseController
{
    private readonly IPersonalCalendarManager _manager;

    public PersonalCalendarController(IPersonalCalendarManager manager)
    {
        _manager = manager;
    }

    /// <remarks>
    /// Retrieves all personal calendar events by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpGet("events")]
    [ProducesResponseType(typeof(IEnumerable<CalendarEvent>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetPersonalCalendarEvents([FromQuery] DateTime dateTimeStart, [FromQuery] DateTime dateTimeEnd, [FromQuery] string? calendarId = null)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();

            if(!string.IsNullOrEmpty(calendarId))
            {
                return await _manager.GetEventsOnCalendarId(userId, calendarId, dateTimeStart, dateTimeEnd);
            }

            return await _manager.GetEvents(userId, dateTimeStart, dateTimeEnd);
        });

        return response;
    }


    /// <remarks>
    /// Retrieves all personal calendars by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PersonalCalendar>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetPersonalCalendars()
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _manager.GetCalendars(userId);
        });

        return response;
    }

    /// <remarks>
    /// Create a new personal calendar by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpPost]
    [ProducesResponseType(typeof(PersonalCalendar), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddPersonalCalendar([FromBody] PersonalCalendarWithCredential input)
    {
        var response = await HandleRequest(async () =>
        {
            var userId = User.GetUserId();
            return await _manager.CreateCalendar(userId, input);
        });

        return response;
    }
    
    /// <remarks>
    /// Delete personal calendar by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpDelete("{calendarId}")]
    [ProducesResponseType(typeof(CalendarEvent), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeletePersonalCalendar(string calendarId)
    {
        var response = await HandleRequest(async () =>
        {
            var userId = User.GetUserId();
            return await _manager.DeleteCalendar(userId, calendarId);
        });

        return response;
    }

}
