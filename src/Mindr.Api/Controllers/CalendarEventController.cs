using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mindr.Api.Extensions;
using Mindr.Api.Models;
using Mindr.Api.Services.CalendarEvents;
using Mindr.Domain.Enums;

using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.CalendarEvent;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mindr.Api.Controllers;

[Authorize]
public class CalendarEventController : BaseController
{
    private readonly ICalendarEventManager _calendarEventManager;

    public CalendarEventController(ICalendarEventManager connectorManager)
    {
        _calendarEventManager = connectorManager;
    }

    /// <remarks>
    /// Retrieves all personal calendar events by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CalendarEvent), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();

            return await _calendarEventManager.GetById(userId, id);
        });

        return response;
    }

    /// <remarks>
    /// Retrieves all personal calendar events by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpGet("{calendarId}")]
    [ProducesResponseType(typeof(IEnumerable<CalendarEvent>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll([FromRoute]string calendarId)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _calendarEventManager.GetEventsOnCalendarId(userId, calendarId);
        });

        return response;
    }

    /// <remarks>
    /// Create a new personal calendar events by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpPost]
    [ProducesResponseType(typeof(CalendarEvent), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] CalendarEventDTO input)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _calendarEventManager.Create(userId, input);
        });

        return response;
    }

    /// <remarks>
    /// Update personal calendar events by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CalendarEvent), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] CalendarEventDTO input)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _calendarEventManager.Update(userId, id, input);
        });

        return response;
    }

    /// <remarks>
    /// Delete personal calendar events by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(CalendarEvent), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _calendarEventManager.Delete(userId, id);
        });

        return response;
    }

}
