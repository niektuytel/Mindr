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
using System;
using Mindr.Api.Models.ConnectorEvents;
using Mindr.Api.Migrations;

namespace Mindr.Api.Services.CalendarEvents
{
    public class PersonalCalendarManager : IPersonalCalendarManager
    {
        private readonly HttpClient _httpClient;
        private readonly ICalendarClient _googleClient;
        private readonly IConnectorEventManager _connectorEventManager;
        private readonly IPersonalCalendarValidator _validator;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public PersonalCalendarManager(IHttpClientFactory factory, ICalendarClient googleCalendarClient, IConnectorEventManager connectorEventManager, IPersonalCalendarValidator connectorValidator, IMapper mapper, ApplicationContext context)
        {
            _httpClient = factory.CreateClient(nameof(PersonalCalendarManager));
            _googleClient = googleCalendarClient;
            _connectorEventManager = connectorEventManager;
            _validator = connectorValidator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<PersonalCalendar>> GetCalendars(string userId)
        {
            var personalCalendars = new List<PersonalCalendar>();
            _validator.ThrowOnInvalidUserId(userId);

            return _context.PersonalCalendars.Where(item => item.UserId == userId);
        }

        public async Task<IEnumerable<PersonalCalendar>> GetExternalCalendars(string userId)
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

        public async Task<PersonalCalendar> InsertCalendar(string userId, PersonalCalendar input)
        {
            _validator.ThrowOnInvalidUserId(userId);
            input.UserId = userId;

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

            _validator.ThrowOnNullPersonalCredential(userId, input.CalendarId, credential);

            _context.PersonalCalendars.Add(input);
            await _context.SaveChangesAsync();
            return input;
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


        public async Task<IEnumerable<CalendarAppointment>> GetAppointments(string userId, DateTime dateTimeStart, DateTime dateTimeEnd, string? calendarId = null)
        {
            _validator.ThrowOnInvalidUserId(userId);
            
            var calendars = await _context.PersonalCalendars
                .Where(item => 
                    item.UserId == userId && 
                    (string.IsNullOrEmpty(calendarId) || item.CalendarId.ToLower() == calendarId.ToLower())// when not null, use
                )
                .ToArrayAsync();

            var appointments = new List<CalendarAppointment>();
            foreach (var calendar in calendars)
            {
                var credential = await _context.PersonalCredentials
                    .FirstOrDefaultAsync(item => 
                        item.Id == calendar!.CredentialId
                    );

                _validator.ThrowOnNullPersonalCredential(userId, calendarId, credential);

                var items = calendar.From switch
                {
                    Domain.Enums.CalendarFrom.Mindr => throw new NotImplementedException("Calendar Mindr not implemented"),
                    Domain.Enums.CalendarFrom.Google => await _googleClient.GetCalendarAppointments(credential!, calendar.CalendarId, dateTimeStart, dateTimeEnd),
                    Domain.Enums.CalendarFrom.Microsoft => throw new NotImplementedException("Calendar Microsoft not implemented"),
                    _ => throw new NotImplementedException($"Unknown Calendar type:{calendar.From}"),
                };

                appointments.AddRange(items);
            }

            return appointments;
        }

        public async Task<CalendarAppointment> InsertAppointment(string userId, string calendarId, CalendarAppointment input)
        {
            _validator.ThrowOnInvalidUserId(userId);

            var calendar = await _context.PersonalCalendars.FirstOrDefaultAsync(item =>
                item.UserId == userId &&
                item.CalendarId == calendarId
            );
            _validator.ThrowOnNullPersonalCalendar(userId, calendarId, calendar);

            var credential = await _context.PersonalCredentials.FirstOrDefaultAsync(item =>
                item.Id == calendar!.CredentialId
            );
            _validator.ThrowOnNullPersonalCredential(userId, calendarId, credential);

            var appointment = calendar!.From switch
            {
                Domain.Enums.CalendarFrom.Mindr => throw new NotImplementedException("Calendar Mindr not implemented"),
                Domain.Enums.CalendarFrom.Google => await _googleClient.InsertCalendarAppointment(credential!, calendar.CalendarId, input),
                Domain.Enums.CalendarFrom.Microsoft => throw new NotImplementedException("Calendar Microsoft not implemented"),
                _ => throw new NotImplementedException($"Unknown Calendar type:{calendar.From}"),
            };

            // Finally insert all connector events
            foreach (var connectorEvent in appointment.ConnectorEvents)
            {
                await _connectorEventManager.Insert(userId, new ConnectorEventOnCreate(connectorEvent));
            }

            return appointment;
        }

        public async Task<CalendarAppointment> UpdateAppointment(string userId, string calendarId, string appointmentId, CalendarAppointment input)
        {
            _validator.ThrowOnInvalidUserId(userId);

            var calendar = await _context.PersonalCalendars.FirstOrDefaultAsync(item =>
                item.UserId == userId &&
                item.CalendarId == calendarId
            );
            _validator.ThrowOnNullPersonalCalendar(userId, calendarId, calendar);

            var credential = await _context.PersonalCredentials.FirstOrDefaultAsync(item =>
                item.Id == calendar!.CredentialId
            );
            _validator.ThrowOnNullPersonalCredential(userId, calendarId, credential);

            var appointment = calendar!.From switch
            {
                Domain.Enums.CalendarFrom.Mindr => throw new NotImplementedException("Calendar Mindr not implemented"),
                Domain.Enums.CalendarFrom.Google => await _googleClient.UpdateCalendarAppointment(credential!, calendar.CalendarId, appointmentId, input),
                Domain.Enums.CalendarFrom.Microsoft => throw new NotImplementedException("Calendar Microsoft not implemented"),
                _ => throw new NotImplementedException($"Unknown Calendar type:{calendar.From}"),
            };

            // TODO: Define Event steps
            // {
            //var eventSteps = new List<ConnectorEventStep>()
            //{
            //    new ConnectorEventStep()
            //    {
            //        Key = Domain.Enums.EventType.OnDateTime,
            //        StepIndex = 1
            //    }
            //};
            //};

            // Finally upsert all connector events
            foreach (var connectorEvent in appointment.ConnectorEvents)
            {
                await _connectorEventManager.Upsert(userId, connectorEvent);
            }

            return appointment;
        }

        public async Task<CalendarAppointment> DeleteAppointment(string userId, string calendarId, string appointmentId)
        {
            _validator.ThrowOnInvalidUserId(userId);

            var calendar = await _context.PersonalCalendars.FirstOrDefaultAsync(item =>
                item.UserId == userId &&
                item.CalendarId == calendarId
            );
            _validator.ThrowOnNullPersonalCalendar(userId, calendarId, calendar);

            var credential = await _context.PersonalCredentials.FirstOrDefaultAsync(item => 
                item.Id == calendar!.CredentialId
            );
            _validator.ThrowOnNullPersonalCredential(userId, calendarId, credential);

            var appointment = calendar!.From switch
            {
                Domain.Enums.CalendarFrom.Mindr => throw new NotImplementedException("Calendar Mindr not implemented"),
                Domain.Enums.CalendarFrom.Google => await _googleClient.DeleteCalendarAppointment(credential!, calendar.CalendarId, appointmentId),
                Domain.Enums.CalendarFrom.Microsoft => throw new NotImplementedException("Calendar Microsoft not implemented"),
                _ => throw new NotImplementedException($"Unknown Calendar type:{calendar.From}"),
            };

            // Finally delete all connector events
            foreach (var connectorEvent in appointment.ConnectorEvents)
            {
                await _connectorEventManager.Delete(userId, connectorEvent.Id);
            }

            return appointment;
        }


        public async Task<IEnumerable<ConnectorEvent>> GetConnectorEvents(string userId, string? calendarId)
        {
            _validator.ThrowOnInvalidUserId(userId);

            var calendars = await _context.PersonalCalendars
                .Where(item =>
                    item.UserId == userId &&
                    (string.IsNullOrEmpty(calendarId) || item.CalendarId.ToLower() == calendarId.ToLower())// when not null, use
                )
                .ToArrayAsync();

            var connectorEvents = new List<ConnectorEvent>();
            foreach (var calendar in calendars)
            {
                var events = await _connectorEventManager.GetAllByEventId(userId, calendarId);
                connectorEvents.AddRange(events);
            }   

            return connectorEvents;
        }

        public async Task<ConnectorEvent> InsertConnectorEvent(string userId, string calendarId, ConnectorEvent input)
        {
            _validator.ThrowOnInvalidUserId(userId);

            var calendar = await _context.PersonalCalendars.FirstOrDefaultAsync(item =>
                item.UserId == userId &&
                item.CalendarId == calendarId
            );

            // TODO: Define Event steps
            // {
                //EventVariables = new List<ConnectorEventVa>()
            //    {
            //        new ConnectorEventVariable()
            //        {
            //            Key = Domain.Enums.EventType.OnRecurring,
            //            StepIndex = 0,
            //        },
            //        new ConnectorEventVariable()
            //        {
            //            Key = Domain.Enums.EventType.OnDateTime,
            //            StepIndex = 1
            //        }
            //    }
            //};


            var onCreateItem = new ConnectorEventOnCreate(input);
            onCreateItem.EventId = calendarId;

            return await _connectorEventManager.Insert(userId, onCreateItem);
        }

        public async Task<ConnectorEvent> DeleteConnectorEvent(string userId, Guid connectorEventId)
        {
            _validator.ThrowOnInvalidUserId(userId);
            return await _connectorEventManager.Delete(userId, connectorEventId);
        }

        public async Task<ConnectorEvent> UpdateConnectorEvent(string userId, Guid connectorEventId, ConnectorEvent input)
        {
            _validator.ThrowOnInvalidUserId(userId);

            var onUpdateItem = new ConnectorEventOnUpdate(input);


            // TODO: Define Event steps
            // {
            //EventVariables = new List<ConnectorEventVa>()
            //    {
            //        new ConnectorEventVariable()
            //        {
            //            Key = Domain.Enums.EventType.OnRecurring,
            //            StepIndex = 0,
            //        },
            //        new ConnectorEventVariable()
            //        {
            //            Key = Domain.Enums.EventType.OnDateTime,
            //            StepIndex = 1
            //        }
            //    }
            //};

            return await _connectorEventManager.Update(userId, connectorEventId, onUpdateItem);
        }

    }
}