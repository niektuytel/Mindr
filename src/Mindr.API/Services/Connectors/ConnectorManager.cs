﻿using Hangfire;
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
using NuGet.Packaging;
using Force.DeepCloner;
using System.Data.SqlClient;

namespace Mindr.Api.Services.Connectors
{
    public class ConnectorManager : IConnectorManager
    {
        private readonly IConnectorValidator _connectorValidator;
        private readonly IConnectorDriver _connectorDriver;
        private readonly IConnectorEventManager _eventClient;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public ConnectorManager(
            IConnectorValidator connectorValidator,
            IConnectorDriver connectorDriver, 
            IMapper mapper, 
            IConnectorEventManager eventClient, 
            ApplicationContext context
        ) {
            _connectorValidator = connectorValidator;
            _connectorDriver = connectorDriver;
            _mapper = mapper;
            _eventClient = eventClient;
            _context = context;
        }

        public async Task<Connector> GetById(string userId, Guid id)
        {
            var entity = _context.Connectors
                .Include(item => item.Variables)
                .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Variables)
                .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Url)
                .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Header)
                .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Body).ThenInclude(item => item.Options).ThenInclude(item => item.Raw)

                // TODO: issue TimeOut! (using same objects mayby cause infinity join issue)
                //.Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.Variables)
                //.Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Variables)
                //.Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Url).ThenInclude(item => item.Query)
                //.Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Header)
                //.Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Body).ThenInclude(item => item.Options).ThenInclude(item => item.Raw)
                //.Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.Header)
                //.Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.Cookie)

                .Where(item => (item.IsPublic || item.CreatedBy == userId))
                .FirstOrDefault(item => item.Id == id);

            _connectorValidator.ThrowOnNullConnector(id, entity);
            return entity!;
        }

        public async Task<IEnumerable<ConnectorBriefDTO>> GetAll(string userId)
        {
            _connectorValidator.ThrowOnInvalidUserId(userId);

            var entities = await _context.Connectors
                .Include(item => item.Variables)
                .Where(item => (item.IsPublic || item.CreatedBy == userId))
                .ToListAsync();

            var items = _mapper.Map<IEnumerable<Connector>, IEnumerable<ConnectorBriefDTO>>(entities);
            return items;
        }

        public async Task<IEnumerable<ConnectorEvent>> GetAllAsEvent(string userId, int limit=10)
        {
            _connectorValidator.ThrowOnInvalidUserId(userId);

            var items = await _context.Connectors
                .Include(item => item.Variables)
                .Where(item => (item.IsPublic || item.CreatedBy == userId))
                .Select(item => item.AsEvent(userId, item.CreatedBy))
                .Take(limit)
                .ToListAsync();

            return items;
        }

        public async Task<IEnumerable<ConnectorBriefDTO>> GetAllByQuery(string userId, string? query)
        {
            _connectorValidator.ThrowOnInvalidQuery(query);

            var entities = await _context.Connectors
                .Include(item => item.Variables)
                .Where(item => (item.IsPublic || item.CreatedBy == userId))
                .Where(item => item.Name.ToLower().Contains(query!))
                .ToListAsync();

            // As this is an global search not on personal user we remove the credentials
            foreach (var entity in entities)
            {
                foreach (var variable in entity.Variables)
                {
                    variable.Value = "";
                }
            }

            var items = _mapper.Map<IEnumerable<Connector>, IEnumerable<ConnectorBriefDTO>>(entities);
            return items;
        }

        public async Task<IEnumerable<ConnectorEvent>> GetAllByQueryAsEvent(string userId, string? query, int limit = 10)
        {
            _connectorValidator.ThrowOnInvalidQuery(query);

            var items = await _context.Connectors
                .Include(item => item.Variables)
                .Where(item => (item.IsPublic || item.CreatedBy == userId))
                .Where(item => item.Name.ToLower().Contains(query!))
                .Take(limit)
                .Select(item => item.AsEvent(userId, item.CreatedBy))
                .ToListAsync();

            return items;
        }

        public async Task<IEnumerable<ConnectorBriefDTO>> GetAllByEventId(string userId, string eventId)
        {
            var events = await _eventClient.GetAllByEventId(userId, eventId);
            var connectorIds = events.Select(item => item.ConnectorId).Distinct();

            var entities = await _context.Connectors
                .Include(item => item.Variables)
                .Where(item => (item.IsPublic || item.CreatedBy == userId))
                .Where(item => connectorIds.Contains(item.Id))
                .ToListAsync();

            var items = _mapper.Map<IEnumerable<Connector>, IEnumerable<ConnectorBriefDTO>>(entities);
            return items;
        }

        public async Task<Connector> Create(string userId, ConnectorOnCreate input)
        {
            await _connectorValidator.ThrowOnInvalidName(userId, input.Name);

            var entity = new Connector()
            {
                CreatedBy = userId,
                Name = input.Name,
                Description = input.Description
            };

            _context.Connectors.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<ConnectorOverviewDTO> GetOverview(string userId, Guid id)
        {
            var entity = await _context.Connectors
                .Include(item => item.Variables)
                .Where(item => (item.IsPublic || item.CreatedBy == userId))
                .FirstOrDefaultAsync(x => x.Id == id);

            _connectorValidator.ThrowOnNullConnector(id, entity);

            var item = _mapper.Map<Connector, ConnectorOverviewDTO>(entity!);
            return item;
        }

        public async Task<ConnectorOverviewDTO> UpdateOverview(string userId, Guid id, ConnectorOverviewDTO input)
        {
            var entity = await _context.Connectors
                //.Include(item => item.Variables)
                .Where(item => (item.IsPublic || item.CreatedBy == userId))
                .FirstOrDefaultAsync(x => x.Id == id);

            _connectorValidator.ThrowOnNullConnector(id, entity);

            entity!.Name = input.Name;
            entity.Description = input.Description;
            entity.IsPublic = input.IsPublic;

            _context.ConnectorVariables.UpdateRange(input.Variables);
            _context.Connectors.Update(entity);
            await _context.SaveChangesAsync();

            entity.Variables = input.Variables.ToList();
            var item = _mapper.Map<Connector, ConnectorOverviewDTO>(entity);
            return item;
        }

        public async Task<IEnumerable<HttpItem>> UpdateHttpItems(string userId, Guid id, IEnumerable<HttpItem> items)
        {
            var entity = await GetById(userId, id);

            // remove pipeline
            _context.HttpItems.RemoveRange(entity.Pipeline);
            await _context.SaveChangesAsync();

            // create pipeline
            var pipeline = new List<HttpItem>();
            foreach (var item in items)
            {
                //create a new instance to avoid tracking conflicts
                pipeline.Add(new HttpItem(Guid.NewGuid(), item));
            }
            entity.Pipeline = pipeline;

            _context.HttpItems.AddRange(pipeline);
            _context.Connectors.Update(entity);
            await _context.SaveChangesAsync();

            return items;
        }

        public async Task<Connector> Delete(string userId, Guid id)
        {
            var entity = await GetById(userId, id);

            _connectorValidator.ThrowOnInvalidUserId(userId);
            _connectorValidator.ThrowOnNullConnector(id, entity);

            // delete entity events
            var events = await _eventClient.GetAllByConnectorId(userId, id);
            foreach (var item in events)
            {
                await _eventClient.Delete(userId, item.Id);
            }

            // delete entity
            _context.Connectors.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

    }
}