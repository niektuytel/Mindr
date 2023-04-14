using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.ExternalConnectors;
using Mindr.Api.Migrations;
using Mindr.Api.Models;
using Mindr.Api.Persistence;
using Mindr.API.Exceptions;
using Mindr.Core.Enums;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Mindr.Core.Services.Connectors;
using System;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;

namespace Mindr.Api.Services.ConnectorEvents;

public class ConnectorEventManager : IConnectorEventManager
{
    private readonly IConnectorEventValidator _connectorEventValidator;
    private readonly IConnectorEventDriver _connectorEventDriver;
    private readonly IBackgroundJobClient _backgroundJobs;
    private readonly ApplicationContext _context;

    public ConnectorEventManager(IConnectorEventValidator eventValidator, IConnectorEventDriver eventDriver, IHttpCollectionClient collectionClient, IBackgroundJobClient backgroundJobs, ApplicationContext context)
    {
        _connectorEventValidator = eventValidator;
        _connectorEventDriver = eventDriver; 
        _backgroundJobs = backgroundJobs;
        _context = context;
    }

    public async Task<ConnectorEvent> GetById(string userId, Guid id)
    {
        var entity = await _context.ConnectorEvents
            .Include(x => x.EventParameters)
            .Include(x => x.ConnectorVariables)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

        _connectorEventValidator.ThrowOnNullEvent(userId, id, entity);

        return entity!;
    }

    public async Task<IEnumerable<ConnectorEvent>> GetAll(string userId)
    {
        _connectorEventValidator.ThrowOnInvalidUserId(userId);

        var items = await _context.ConnectorEvents
            .Include(x => x.EventParameters)
            .Include(x => x.ConnectorVariables)
            .Where(x => x.UserId == userId)
            .ToListAsync();

        return items;
    }

    public async Task<IEnumerable<ConnectorEvent>> GetAllByEventId(string userId, string eventId)
    {
        _connectorEventValidator.ThrowOnInvalidUserId(userId);
        _connectorEventValidator.ThrowOnInvalidEventId(eventId);

        var items = await _context.ConnectorEvents
            .Include(x => x.EventParameters)
            .Include(x => x.ConnectorVariables)
            .Where(x => x.UserId == userId && x.EventId == eventId)
            .ToListAsync();

        return items;
    }

    public async Task<IEnumerable<ConnectorEvent>> GetAllByQuery(string userId, string query)
    {
        _connectorEventValidator.ThrowOnInvalidUserId(userId);
        _connectorEventValidator.ThrowOnInvalidQuery(query);

        var items = await _context.ConnectorEvents
            .Include(x => x.EventParameters)
            .Include(x => x.ConnectorVariables)
            .Where(x => x.UserId == userId && x.ConnectorName.ToLower().Contains(query))
            .ToListAsync();

        return items;
    }

    public async Task<ConnectorEvent> UpdateById(string userId, Guid id, ConnectorEventOnUpdate input)
    {
        _connectorEventValidator.ThrowOnInvalidConnectorVariables(input.ConnectorVariables);

        var entity = await _context.ConnectorEvents
                .Include(x => x.EventParameters)
                .Include(x => x.ConnectorVariables)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

        _connectorEventValidator.ThrowOnNullEvent(userId, id, entity);

        // remove variables
        _context.ConnectorVariables.RemoveRange(entity!.ConnectorVariables);
        await _context.SaveChangesAsync();

        // create variables
        entity!.ConnectorVariables = input.ConnectorVariables;

        var jobId = await _connectorEventDriver.ProcessConnectorEventAsync(entity);
        if (!string.IsNullOrEmpty(jobId))
        {
            entity.JobId = jobId;
            _context.ConnectorEvents.Update(entity);
            await _context.SaveChangesAsync();
        }

        return entity;
    }

    public async Task<ConnectorEvent> Create(string userId, ConnectorEventOnCreate input)
    {
        _connectorEventValidator.ThrowOnInvalidUserId(userId);
        _connectorEventValidator.ThrowOnInvalidEventId(input.EventId);
        _connectorEventValidator.ThrowOnInvalidEventParameters(input.EventParameters);
        _connectorEventValidator.ThrowOnInvalidConnectorVariables(input.ConnectorVariables);
        _connectorEventValidator.ThrowOnNotUniqueConnectorVariables(input.ConnectorVariables);

        var connector = await _context.Connectors.FirstOrDefaultAsync(item => item.IsPublic && item.Id == input.ConnectorId);
        _connectorEventValidator.ThrowOnNullConnector(input.ConnectorId, connector);

        // process
        var entity = input.ToConnectorEvent(userId, connector!);
        var jobId = await _connectorEventDriver.ProcessConnectorEventAsync(entity);

        if (!string.IsNullOrEmpty(jobId))
        {
            entity.JobId = jobId;
            _context.ConnectorEvents.Add(entity);
            await _context.SaveChangesAsync();
        }

        return entity;
    }

    public async Task<ConnectorEvent> DeleteById(string userId, Guid id)
    {
        var entity = _context.ConnectorEvents
                .Include(x => x.EventParameters)
                .Include(x => x.ConnectorVariables)
                .FirstOrDefault(x => x.Id == id && x.UserId == userId);

        _connectorEventValidator.ThrowOnNullEvent(userId, id, entity);

        // process
        if (!string.IsNullOrEmpty(entity!.JobId))
        {
            _backgroundJobs.Delete(entity.JobId);
        }

        _context.ConnectorEvents.Remove(entity!);
        await _context.SaveChangesAsync();
        return entity;
    }
}
