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
using Mindr.Domain.Models.DTO.PersonalCredential;
using Mindr.Domain.Models.DTO.CalendarEvent;

namespace Mindr.Api.Services.CalendarEvents
{
    public class CalendarEventManager : ICalendarEventManager
    {
        private readonly HttpClient _httpClient;
        private readonly IGoogleCalendarClient _googleClient;
        private readonly ICalendarEventValidator _connectorValidator;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public CalendarEventManager(IHttpClientFactory factory, IGoogleCalendarClient googleCalendarClient, ICalendarEventValidator connectorValidator, IMapper mapper, ApplicationContext context)
        {
            _httpClient = factory.CreateClient(nameof(CalendarEventManager));
            _googleClient = googleCalendarClient;
            _connectorValidator = connectorValidator;
            _mapper = mapper;
            _context = context;
        }

        public async Task<CalendarEvent> GetById(string userId, Guid id)
        {
            _connectorValidator.ThrowOnInvalidUserId(userId);

            var entity = await _context.CalendarEvents
                .Where(item => item.UserId == userId)
                .Where(item => item.Id == id)
                .FirstOrDefaultAsync();

            _connectorValidator.ThrowOnNullCalendarEvent(id, entity);

            return entity!;
        }

        public async Task<IEnumerable<CalendarEvent>> GetEventsOnCalendarId(string userId, string calendarId)
        {
            _connectorValidator.ThrowOnInvalidUserId(userId);

            var googleCalendarCredentials = _context.PersonalCredentials
                .Where(item => item.UserId == userId)
                .Where(item => item.Target == Domain.Enums.CredentialTarget.GoogleCalendar);

            foreach (var credential in googleCalendarCredentials)
            {
                var accessToken = _googleClient.GetAccessToken(credential.RefreshToken);
                await Console.Out.WriteLineAsync(   );




                //var response = await _httpClient.SendAsync();

                //RestClient restClient = new RestClient();
                //RestRequest request = new RestRequest();

                //request.AddQueryParameter("client_id", "value");
                //request.AddQueryParameter("client_secret", "value");
                //request.AddQueryParameter("grant_type", "refresh_token");
                //request.AddQueryParameter("refresh_token", "value");

                //restClient.BaseUrl = new System.Uri("https://oauth2.googleapis.com/token");
                //restClient.Post(request);
            }

            return null;
        }

        public async Task<CalendarEvent> Create(string userId, CalendarEventDTO input)
        {
            _connectorValidator.ThrowOnInvalidUserId(userId);
            var entity = new CalendarEvent(userId, input);

            _context.CalendarEvents.Add(entity);
            await _context.SaveChangesAsync();

            return entity!;
        }

        public async Task<CalendarEvent> Update(string userId, Guid id, CalendarEventDTO input)
        {
            _connectorValidator.ThrowOnInvalidUserId(userId);
            var entity = await _context.CalendarEvents
                .Where(item => item.UserId == userId)
                .FirstOrDefaultAsync(x => x.Id == id);

            _connectorValidator.ThrowOnNullCalendarEvent(id, entity);

            //entity!.AccessToken = input.AccessToken;
            //entity!.ExpiresIn = input.ExpiresIn;
            //entity!.RefreshToken = input.RefreshToken;
            //entity!.Scope = input.Scope;
            //entity!.TokenType = input.TokenType;

            _context.CalendarEvents.Update(entity!);
            await _context.SaveChangesAsync();

            return entity!;
        }

        public async Task<CalendarEvent> Delete(string userId, Guid id)
        {
            _connectorValidator.ThrowOnInvalidUserId(userId);
            var entity = await _context.CalendarEvents
                .Where(item => item.UserId == userId)
                .FirstOrDefaultAsync(x => x.Id == id);

            _connectorValidator.ThrowOnNullCalendarEvent(id, entity);

            // delete entity
            _context.CalendarEvents.Remove(entity!);
            await _context.SaveChangesAsync();

            return entity!;
        }

    }
}