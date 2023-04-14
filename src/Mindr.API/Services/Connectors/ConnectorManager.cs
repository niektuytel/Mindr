using Hangfire;
using Mindr.API.Exceptions;
using Mindr.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Graph.ExternalConnectors;
using Mindr.Api.Services.ConnectorEvents;
using System.Net;
using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.HttpRunner.Models;
using Mindr.Api.Models.Connectors;
using Mindr.Core.Models.Connectors;

namespace Mindr.Api.Services.Connectors
{
    public class ConnectorManager : IConnectorManager
    {
        private readonly IConnectorValidator _connectorValidator;
        private readonly IConnectorDriver _connectorDriver;
        private readonly IConnectorEventManager _eventClient;
        private readonly IMapper _mapper;
        //private readonly ConnectorPipelineDriver _helper;
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

            //_helper = new ConnectorPipelineDriver();
        }

        public async Task<Connector> GetById(string userId, Guid id)
        {
            var entity = await _context.Connectors
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
                .FirstOrDefaultAsync(item => item.CreatedBy == userId && item.Id == id);

            _connectorValidator.ThrowOnNullConnector(id, entity);

            return entity!;
        }

        public async Task<IEnumerable<ConnectorBriefDTO>> GetAllByQuery(string? query)
        {
            _connectorValidator.ThrowOnInvalidQuery(query);

            var entities = await _context.Connectors
                .Include(item => item.Variables)
                .Where(item => item.Name.ToLower().Contains(query!))
                .ToListAsync();

            var items = _mapper.Map<IEnumerable<Connector>, IEnumerable<ConnectorBriefDTO>>(entities);
            return items;
        }

        public async Task<IEnumerable<ConnectorBriefDTO>> GetAllByEventId(string userId, string eventId)
        {
            var events = await _eventClient.GetAllByEventId(userId, eventId);
            var connectorIds = events.Select(item => item.ConnectorId).Distinct();

            var entities = await _context.Connectors
                .Include(item => item.Variables)
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
                .FirstOrDefaultAsync(x => x.CreatedBy == userId && x.Id == id);

            _connectorValidator.ThrowOnNullConnector(id, entity);

            var item = _mapper.Map<Connector, ConnectorOverviewDTO>(entity!);
            return item;
        }

        public async Task<ConnectorOverviewDTO> UpdateOverview(string userId, Guid id, ConnectorOverviewDTO input)
        {
            var entity = await _context.Connectors
                .Include(item => item.Variables)
                .FirstOrDefaultAsync(x => x.CreatedBy == userId && x.Id == id);

            _connectorValidator.ThrowOnNullConnector(id, entity);

            entity!.Name = input.Name;
            entity.Description = input.Description;

            _context.ConnectorVariables.UpdateRange(input.Variables);
            _context.Connectors.Update(entity);
            await _context.SaveChangesAsync();

            var item = _mapper.Map<Connector, ConnectorOverviewDTO>(entity);
            return item;
        }

        public async Task<IEnumerable<HttpItem>> UpdateHttpItems(string userId, Guid id, IEnumerable<HttpItem> input)
        {
            var entity = await GetById(userId, id);

            // remove pipeline
            _context.HttpItems.RemoveRange(entity.Pipeline);
            await _context.SaveChangesAsync();

            // create pipeline
            var items = await _connectorDriver.AutoCompletePipeline(entity.Variables, input);

            entity.Pipeline = items;
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
                await _eventClient.DeleteById(userId, item.Id);
            }

            // delete entity
            _context.Connectors.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

    }
}