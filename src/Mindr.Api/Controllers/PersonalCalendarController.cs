﻿using Microsoft.AspNetCore.Authorization;
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
    [ProducesResponseType(typeof(CalendarAppointment), (int)HttpStatusCode.OK)]
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


    /// <remarks>
    /// Retrieves all personal calendar events by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpGet("appointments")]
    [ProducesResponseType(typeof(IEnumerable<CalendarAppointment>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(PersonalCredential), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetCalendarAppointments(
        [FromQuery] DateTime dateTimeStart, 
        [FromQuery] DateTime dateTimeEnd, 
        [FromQuery] string? calendarId = null
    )
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _manager.GetAppointments(userId, dateTimeStart, dateTimeEnd, calendarId);
        });

        return response;
    }

    /// <remarks>
    /// Insert a new personal calendar event by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpPost("{calendarId}/appointment")]
    [ProducesResponseType(typeof(CalendarAppointment), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(PersonalCredential), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> InsertCalendarAppointments(
        [FromRoute] string calendarId,
        [FromBody] CalendarAppointment input 
    ){
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _manager.InsertAppointment(userId, calendarId, input);
        });

        return response;
    }

    /// <remarks>
    /// Update personal calendar event by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpPut("{calendarId}/appointment/{appointmentId}")]
    [ProducesResponseType(typeof(CalendarAppointment), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(PersonalCredential), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateCalendarAppointments(
        [FromRoute] string calendarId,
        [FromRoute] string appointmentId, 
        [FromBody] CalendarAppointment input
    ){
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _manager.UpdateAppointment(userId, calendarId, appointmentId, input);
        });

        return response;
    }


    /// <remarks>
    /// Delete personal calendar event by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpDelete("{calendarId}/appointment/{appointmentId}")]
    [ProducesResponseType(typeof(CalendarAppointment), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(PersonalCredential), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteCalendarAppointments(
        [FromRoute] string calendarId,
        [FromRoute] string appointmentId
    )
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _manager.DeleteAppointment(userId, calendarId, appointmentId);
        });

        return response;
    }

}
