using Hangfire;
using Mindr.API.Enums;
using Mindr.API.Exceptions;
using Mindr.Api.Persistence;
using Mindr.Core.Models.Connector;
using Mindr.Core.Services.Connectors;
using Microsoft.AspNetCore.Mvc;
using Mindr.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Graph.ExternalConnectors;
using Mindr.Core.Models.Connector.Http;

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

        public async Task<Connector?> Get(Guid connectorId)
        {
            // TODO: Use No-SQL database (better for searching as this will been re-used mas well?) [MongoDB]
            var connector = await _context.Connectors
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
                .FirstOrDefaultAsync(item => item.Id == connectorId);

            return connector;
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

        public async Task UpdateHttpItems(string userId, Guid connectorId, IEnumerable<HttpItem> payload)
        {
            var items = new List<HttpItem>();
            foreach (var item in payload)
            {
                var httpItem = _context.HttpItems.FirstOrDefault(x => x.Id == item.Id);
                if(httpItem == null)
                {
                    _context.HttpItems.Add(item);
                    items.Add(item);
                }
                else
                {
                    //httpItem.
                    _context.HttpItems.Update(httpItem);
                    items.Add(httpItem);
                }
            }

            _context.SaveChanges();



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
                .FirstOrDefault(item => item.Id == connectorId);

            // 404
            if (connector == null)
            {
                throw new ApiRequestException(ApiResponse.NotFound, $"Missing connector: {connectorId}");
            }

            connector.Pipeline = items.ToArray();
            _context.Connectors.Update(connector);
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
