using Hangfire;
using Mindr.API.Enums;
using Mindr.API.Exceptions;
using Mindr.Api.Persistence;
using Mindr.Core.Models.Connector;
using Mindr.Core.Services.Connectors;
using Microsoft.AspNetCore.Mvc;
using Mindr.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Mindr.Api.Services
{
    public class ConnectorClient : IConnectorClient
    {
        private readonly IConnectorEventClient _eventClient;
        private readonly ApplicationContext _context;

        public ConnectorClient(IConnectorEventClient eventClient, ApplicationContext context)
        {
            _eventClient = eventClient;
            _context = context;
        }

        public async Task<Connector?> GetOverview(Guid connectorId)
        {
            var item = await _context.Connectors
                .Include(item => item.Variables)
                .FirstOrDefaultAsync(x => x.Id == connectorId);

            if(item == null)
            {
                throw new ApiRequestException(ApiResponse.NotFound, $"Did not find connector on {connectorId}");
            }

            return item;
        }

        public async Task UpdateOverview(string userId, Connector payload)
        {
            payload.CreatedBy = userId;
            var entity = _context.Connectors
                //.Include(item => item.Variables)
                .FirstOrDefault(item => item.CreatedBy == payload.CreatedBy && item.Id == payload.Id);

            // insert
            if (entity == null)
            {
                throw new ApiRequestException(ApiResponse.NotFound, $"Did not find connector {payload.Id} on user:{userId}");
            }

            _context.ConnectorVariables.UpdateRange(payload.Variables);

            entity.Name = payload.Name;
            entity.Description = payload.Description;
            _context.Connectors.Update(entity);

            _context.SaveChanges();
        }

        public async Task<IEnumerable<Connector>> GetAll(string userId, string? eventId = null, string? query = null)
        {
            var events = await _eventClient.GetAll(userId, eventId);
            var connectorIds = events.Select(item => item.ConnectorId).Distinct();
            
            if (!string.IsNullOrEmpty(query))
            {
                var items = _context.Connectors.Where(item => 
                    connectorIds.Contains(item.Id) && 
                    item.Name.ToLower().Contains(query)
                );

                return items;
            }

            var items2 = _context.Connectors.Where(item => 
                connectorIds.Contains(item.Id)
            );
            return items2;
        }

        public async Task<IEnumerable<Connector>> GetAllBriefly(string userId)
        {
            return _context.Connectors.Where(x => x.CreatedBy == userId);
        }

        public async Task<Connector> Insert(string userId, Connector payload)
        {
            payload.CreatedBy = userId;
            var entity = _context.Connectors.FirstOrDefault(item => 
                item.CreatedBy == payload.CreatedBy && 
                item.Id == payload.Id
            );

            // insert
            if (entity == null)
            {
                _context.Connectors.Add(payload);
                _context.SaveChanges();
                return payload;
            }


            return entity;
        }

        public async Task Delete(string userId, Guid id)
        {
            var entity = _context.Connectors.FirstOrDefault(item =>
                item.Id == id &&
                item.CreatedBy == userId
            );

            // insert
            if (entity == null)
            {
                throw new ApiRequestException(ApiResponse.NotFound, $"Missing connector: {id} on user: {userId}");
            }

            _context.Connectors.Remove(entity);
            await _context.SaveChangesAsync();
        }



    }
}
