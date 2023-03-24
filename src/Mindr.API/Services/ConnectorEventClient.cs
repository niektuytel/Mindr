using AutoMapper;
using Hangfire;
using Microsoft.Graph;
using Mindr.Api.Persistence;
using Mindr.API.Enums;
using Mindr.API.Exceptions;
using Mindr.Core.Enums;
using Mindr.Core.Models.Connector;
using Mindr.Core.Services.Connectors;
using System.Net.NetworkInformation;

namespace Mindr.API.Services;

public class ConnectorEventClient : IConnectorEventClient
{
    private readonly IHttpCollectionClient _connectorClient;
    private readonly IBackgroundJobClient _backgroundJobs;
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;

    public ConnectorEventClient(IHttpCollectionClient collectionClient, IBackgroundJobClient backgroundJobs, IMapper mapper, ApplicationContext context)
    {
        _connectorClient = collectionClient;
        _backgroundJobs = backgroundJobs;
        _mapper = mapper;
        _context = context;
    }

    private async Task<bool> TryDefaultCall(ConnectorEvent entity)
    {
        // call directly
        if (entity.EventParams.Any())
        {
            return false;
        }

        var connector = _context.Connectors.FirstOrDefault(item => item.Id == entity.ConnectorId);
        if (connector == null)
        {
            throw new ApiRequestException(ApiResponse.BadRequest, $"connector on id: '{entity.ConnectorId}' is unknown");
        }

        // send
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

        var connector = _context.Connectors.FirstOrDefault(item => item.Id == entity.ConnectorId);
        if (connector == null)
        {
            throw new ApiRequestException(ApiResponse.BadRequest, $"connector on id: '{entity.ConnectorId}' is unknown");
        }

        var isvalid = DateTime.TryParse(schedule.Value, out DateTime datetime);
        if (!isvalid)
        {
            throw new ApiRequestException(ApiResponse.BadRequest, $"entity parameter '{nameof(Core.Enums.EventType.OnDateTime)}'");
        }

        // avoid duplicated jobs
        if (!string.IsNullOrEmpty(entity.JobId))
        {
            _backgroundJobs.Delete(entity.JobId);
        }

        // send
        entity.JobId = _backgroundJobs.Schedule(
            () => _connectorClient.SendAsync(connector),
            datetime
        );

        if (onInsert)
        {
            _context.ConnectorEvents.Add(entity);
        }
        else
        {
            _context.ConnectorEvents.Update(entity);
        }

        _context.SaveChanges();
        return true;
    }

    public async Task Upsert(ConnectorEvent @event)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        // TODO: validate: @event

        var entity = _context.ConnectorEvents.FirstOrDefault(x => x.Id == @event.Id);
        var onInsert = entity == null;
        if (onInsert)
        {
            entity = @event;
        }
        else
        {
            // TODO: validate: entity
            entity!.Update(@event);
        }

        var exists = await TryDefaultCall(entity);
        if (exists)
        {
            return;
        }

        exists = await TryCallOnSchedule(entity, onInsert);
        if (exists)
        {
            return;
        }

    }




}
