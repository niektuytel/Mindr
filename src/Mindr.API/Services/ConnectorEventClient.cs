using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Mindr.Api.Persistence;
using Mindr.API.Enums;
using Mindr.API.Exceptions;
using Mindr.Core.Enums;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Mindr.Core.Services.Connectors;
using System.Net.NetworkInformation;

namespace Mindr.API.Services;

public class ConnectorEventClient : IConnectorEventClient
{
    private readonly IHttpCollectionClient _connectorClient;
    private readonly IBackgroundJobClient _backgroundJobs;
    private readonly ApplicationContext _context;

    public ConnectorEventClient(IHttpCollectionClient collectionClient, IBackgroundJobClient backgroundJobs, ApplicationContext context)
    {
        _connectorClient = collectionClient;
        _backgroundJobs = backgroundJobs;
        _context = context;
    }

    private async Task<bool> TryDefaultCall(ConnectorEvent entity)
    {
        // call directly
        if (entity.EventParams.Any())
        {
            return false;
        }

        // TODO: Use No-SQL database (better for searching as this will been re-used mas well?) [MongoDB]
        var connector = _context.Connectors
            .Include(item => item.Variables)
            .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Variables)
            .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Url)
            .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Header)
            .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Body).ThenInclude(item => item.Options).ThenInclude(item => item.Raw)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.Variables)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Variables)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Url).ThenInclude(item => item.Query)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Header)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Body).ThenInclude(item => item.Options).ThenInclude(item => item.Raw)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.Header)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.Cookie)
            .FirstOrDefault(item => item.Id == entity.ConnectorId);

        if (connector == null)
        {
            throw new ApiRequestException(ApiResponse.BadRequest, $"connector on id: '{entity.ConnectorId}' is unknown");
        }

        // send
        connector.Variables = entity.Variables;
        await _connectorClient.SendAsync(connector);
        return true;
    }

    private async Task<bool> TryCallOnSchedule(ConnectorEvent entity, bool onInsert = true)
    {
        var schedule = entity.EventParams.FirstOrDefault(item => item.Type == Core.Enums.EventType.OnDateTime);
        if (schedule == null)
        {
            return false;
        }


        // TODO: Use No-SQL database (better for searching as this will been re-used mas well?) [MongoDB]
        var connector = _context.Connectors
            .Include(item => item.Variables)
            .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Variables)
            .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Url)
            .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Header)
            .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Body).ThenInclude(item => item.Options).ThenInclude(item => item.Raw)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.Variables)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Variables)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Url).ThenInclude(item => item.Query)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Header)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Body).ThenInclude(item => item.Options).ThenInclude(item => item.Raw)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.Header)
            .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.Cookie)
            .FirstOrDefault(item => item.Id == entity.ConnectorId);

        if (connector == null)
        {
            throw new ApiRequestException(ApiResponse.BadRequest, $"connector on id: '{entity.ConnectorId}' is unknown");
        }

        var isvalid = DateTime.TryParse(schedule.Value, out DateTime datetime);
        if (!isvalid)
        {
            throw new ApiRequestException(ApiResponse.BadRequest, $"items parameter '{nameof(Core.Enums.EventType.OnDateTime)}'");
        }

        // TODO: is time still valid to continue time < datetime.now?

        // avoid duplicated jobs
        if (!string.IsNullOrEmpty(entity.JobId))
        {
            _backgroundJobs.Delete(entity.JobId);
        }

        // send
        connector.Variables = entity.Variables;
        entity.JobId = _backgroundJobs.Schedule(
            () => _connectorClient.SendAsync(connector),
            datetime
        );

        if (onInsert)
        {
            var variables = new List<ConnectorVariable>();
            foreach (var variable in entity.Variables)
            {
                variable.Id = Guid.NewGuid();
            }

            entity.Variables = variables.ToArray();
            _context.ConnectorEvents.Add(entity);
        }
        else
        {
            _context.ConnectorEvents.Update(entity);
        }

        _context.SaveChanges();
        return true;
    }

    public async Task<IEnumerable<ConnectorEvent>> GetAll(string userId, string? eventId = null)
    {
        if (!string.IsNullOrEmpty(eventId))
        {
            var items = _context.ConnectorEvents
                .Include(x => x.EventParams)
                .Include(x => x.Variables)
                .Where(x => x.UserId == userId && x.EventId == eventId);

            return items.AsEnumerable();
        }
        else
        {
            var items = _context.ConnectorEvents
                .Include(x => x.EventParams)
                .Include(x => x.Variables)
                .Where(x => x.UserId == userId);

            return items.AsEnumerable();

        }
        
        //throw new ApiRequestException(ApiResponse.NotFound, $"Connector event on user {userId}");

    }

    public async Task Create(ConnectorEvent @event)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        var exists = await TryDefaultCall(@event);
        if (exists)
        {
            return;
        }

        exists = await TryCallOnSchedule(@event, true);
        if (exists)
        {
            return;
        }

    }

    public async Task Update(ConnectorEvent @event)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        // TODO: validate: @event

        var entity = _context.ConnectorEvents
                .Include(x => x.EventParams)
                .Include(x => x.Variables)
                .FirstOrDefault(x => x.Id == @event.Id);

        // TODO: validate: items
        entity!.Update(@event);

        var exists = await TryDefaultCall(entity);
        if (exists)
        {
            return;
        }

        exists = await TryCallOnSchedule(entity, false);
        if (exists)
        {
            return;
        }

    }

    public async Task Delete(Guid id, string userId)
    {
        var entity = _context.ConnectorEvents
                .Include(x => x.EventParams)
                .Include(x => x.Variables)
                .FirstOrDefault(x => x.Id == id && x.UserId == userId) 
            ?? throw new ApiRequestException(ApiResponse.NotFound, $"Connector event {id}");

        _context.ConnectorEvents.Remove(entity);
        await _context.SaveChangesAsync();
        
        // remove from background queue
        if (!string.IsNullOrEmpty(entity.JobId))
        {
            _backgroundJobs.Delete(entity.JobId);
        }
    }


}
