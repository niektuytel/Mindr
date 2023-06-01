using Hangfire;
using Mindr.Api.Exceptions;
using Mindr.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Graph.ExternalConnectors;
using Mindr.Api.Services.ConnectorEvents;
using System.Net;
using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.Domain.HttpRunner.Models;
using Mindr.Api.Models.Connectors;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.Personal;
using Mindr.Domain.Models.DTO.Calendar;

namespace Mindr.Api.Services.CalendarEvents
{
    public class PersonalCalendarManager : IPersonalCalendarManager
    {
        private readonly HttpClient _httpClient;
        private readonly IGoogleCalendarClient _googleClient;
        private readonly IPersonalCalendarValidator _validator;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public PersonalCalendarManager(IHttpClientFactory factory, IGoogleCalendarClient googleCalendarClient, IPersonalCalendarValidator connectorValidator, IMapper mapper, ApplicationContext context)
        {
            _httpClient = factory.CreateClient(nameof(PersonalCalendarManager));
            _googleClient = googleCalendarClient;
            _validator = connectorValidator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<CalendarAppointment>> GetAppointmentsOnCalendarId(string userId, string calendarId, DateTime startDateTime, DateTime endDateTime)
        {
            _validator.ThrowOnInvalidUserId(userId);

            var calendar = await _context.PersonalCalendars.FirstOrDefaultAsync(item => item.UserId == userId && item.CalendarId == calendarId);
            _validator.ThrowOnNullPersonalCalendar(userId, calendarId, calendar);

            var credential = await _context.PersonalCredentials.FirstOrDefaultAsync(item => item.Id == calendar!.CredentialId);
            _validator.ThrowOnNullPersonalCredential(userId, calendarId, credential);

            if (calendar!.From == Domain.Enums.CalendarFrom.Google)
            {
                var events = await _googleClient.GetCalendarAppointment(credential!, startDateTime, endDateTime, calendarId);
                if (events != null)
                {
                    return events;
                }
            }

            return new List<CalendarAppointment>();
        }

        public async Task<IEnumerable<CalendarAppointment>> GetAppointments(string userId, DateTime startDateTime, DateTime endDateTime)
        {
            _validator.ThrowOnInvalidUserId(userId);

            var events = await GetAppointmentsFromGoogle(userId, startDateTime, endDateTime);

            return events;
        }
        
        private async Task<IEnumerable<CalendarAppointment>> GetAppointmentsFromGoogle(string userId, DateTime startDateTime, DateTime endDateTime)
        {
            var events = new List<CalendarAppointment>();
            
            var calendars = await _context.PersonalCalendars.Where(item => item.From == Domain.Enums.CalendarFrom.Google).ToListAsync();
            foreach (var calendar in calendars)
            {
                var credential = await _context.PersonalCredentials.FirstOrDefaultAsync(item => item.UserId == userId && item.Id == calendar.CredentialId);
                _validator.ThrowOnNullPersonalCredential(userId, calendar.CalendarId, credential);

                var response = await _googleClient.GetCalendarAppointment(credential!, startDateTime, endDateTime, calendar.CalendarId);
                if(response == null)
                {
                    continue;
                }

                events.AddRange(response);
            }

            return events;
        }
        
        public async Task<IEnumerable<PersonalCalendar>> GetCalendars(string userId)
        {
            var personalCalendars = new List<PersonalCalendar>();
            _validator.ThrowOnInvalidUserId(userId);

            var credentials = await _context.PersonalCredentials
                .Where(item => item.UserId == userId)
                .ToListAsync();

            foreach (var credential in credentials)
            {
                // Google Credentials
                var googleCalendars = await _googleClient.GetCalendars(credential, userId);
                foreach (var calendar in googleCalendars)
                {
                    calendar.Selected = _context.PersonalCalendars.Select(item => item.CalendarId).Contains(calendar.CalendarId);
                }

                personalCalendars.AddRange(googleCalendars);
            }

            return personalCalendars;
        }

        public async Task<PersonalCalendar> CreateCalendar(string userId, PersonalCalendarWithCredential input)
        {
            _validator.ThrowOnInvalidUserId(userId);

            var calendar = await _context.PersonalCalendars.FirstOrDefaultAsync(item =>
                item.UserId == userId && 
                item.CalendarId == input.CalendarId
            );

            if (calendar != null)
            {
                return calendar;
            }

            var credential = await _context.PersonalCredentials.FirstOrDefaultAsync(item => 
                item.UserId == userId &&
                item.Target == Domain.Enums.CredentialTarget.GoogleCalendar
            );

            if (credential == null)
            {
                // Create
                credential = new PersonalCredential(
                    userId,
                    Domain.Enums.CredentialTarget.GoogleCalendar,
                    input.AccessToken,
                    input.RefreshToken,
                    input.Scope,
                    input.TokenType,
                    input.ExpiresIn
                );

                await _context.PersonalCredentials.AddAsync(credential);
                await _context.SaveChangesAsync();

            }
            else
            {
                // Update
                credential.ExpiresIn = input.ExpiresIn;
                credential.AccessToken = input.AccessToken;
                credential.RefreshToken = input.RefreshToken;
                credential.Scope = input.Scope;
                credential.TokenType = input.TokenType;

                _context.PersonalCredentials.Update(credential);
                await _context.SaveChangesAsync();
            }

            calendar = new PersonalCalendar(
                userId, 
                input.CalendarId, 
                credential.Id, 
                input.CalendarFrom
            );

            _context.PersonalCalendars.Add(calendar);
            await _context.SaveChangesAsync();
            return calendar;
        }

        public async Task<PersonalCalendar> DeleteCalendar(string userId, string calendarId)
        {
            _validator.ThrowOnInvalidUserId(userId);

            var calendar = await _context.PersonalCalendars.FirstOrDefaultAsync(item =>
                item.UserId == userId &&
                item.CalendarId == calendarId
            );
            _validator.ThrowOnNullPersonalCalendar(userId, calendarId, calendar);

            var credential = await _context.PersonalCredentials.FirstOrDefaultAsync(item =>
                item.UserId == userId &&
                item.Id == calendar!.CredentialId
            );
            _validator.ThrowOnNullPersonalCredential(userId, calendarId, credential);

            _context.PersonalCredentials.Remove(credential!);
            _context.PersonalCalendars.Remove(calendar!);
            await _context.SaveChangesAsync();

            return calendar!;
        }


        //public async Task<CalendarEvent> GetById(string userId, Guid id)
        //{
        //    _connectorValidator.ThrowOnInvalidUserId(userId);

        //    var entity = await _context.CalendarEvents
        //        .Where(item => item.UserId == userId)
        //        .Where(item => item.Id == id)
        //        .FirstOrDefaultAsync();

        //    _connectorValidator.ThrowOnNullCalendarEvent(id, entity);

        //    return entity!;
        //}


        //public async Task<CalendarEvent> Create(string userId, CalendarEventDTO input)
        //{
        //    _connectorValidator.ThrowOnInvalidUserId(userId);
        //    var entity = new CalendarEvent(userId, input);

        //    _context.CalendarEvents.Add(entity);
        //    await _context.SaveChangesAsync();

        //    return entity!;
        //}

        //public async Task<CalendarEvent> Update(string userId, Guid id, CalendarEventDTO input)
        //{
        //    _connectorValidator.ThrowOnInvalidUserId(userId);
        //    var entity = await _context.CalendarEvents
        //        .Where(item => item.UserId == userId)
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    _connectorValidator.ThrowOnNullCalendarEvent(id, entity);

        //    //entity!.AccessToken = input.AccessToken;
        //    //entity!.ExpiresIn = input.ExpiresIn;
        //    //entity!.RefreshToken = input.RefreshToken;
        //    //entity!.Scope = input.Scope;
        //    //entity!.TokenType = input.TokenType;

        //    _context.CalendarEvents.Update(entity!);
        //    await _context.SaveChangesAsync();

        //    return entity!;
        //}

        //public async Task<CalendarEvent> Delete(string userId, Guid id)
        //{
        //    _connectorValidator.ThrowOnInvalidUserId(userId);
        //    var entity = await _context.CalendarEvents
        //        .Where(item => item.UserId == userId)
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    _connectorValidator.ThrowOnNullCalendarEvent(id, entity);

        //    // delete entity
        //    _context.CalendarEvents.Remove(entity!);
        //    await _context.SaveChangesAsync();

        //    return entity!;
        //}

    }
}