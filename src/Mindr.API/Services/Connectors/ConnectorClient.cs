using Hangfire;
using Mindr.API.Exceptions;
using Mindr.Api.Persistence;
using Mindr.Core.Models.Connector;
using Mindr.Core.Services.Connectors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Graph.ExternalConnectors;
using Mindr.Core.Models.Connector.Http;
using Mindr.Api.Models;
using Mindr.Api.Services.ConnectorEvents;
using System.Net;

namespace Mindr.Api.Services.Connectors
{
    public class ConnectorClient : IConnectorClient
    {
        private readonly IConnectorEventManager _eventClient;
        //private readonly ConnectorPipelineDriver _helper;
        private readonly ApplicationContext _context;

        public ConnectorClient(IConnectorEventManager eventClient, ApplicationContext context)
        {
            _eventClient = eventClient;
            _context = context;

            //_helper = new ConnectorPipelineDriver();
        }

        public async Task<ConnectorInsertResponse> Create(string userId, ConnectorInsert input)
        {
            // validate & prepare
            var entity = _context.Connectors.FirstOrDefault(item => item.Name == input.Name);
            if (string.IsNullOrEmpty(input.Name) || entity != null)
            {
                var id = _context.Connectors.Count();
                input.Name = $"[Test: {id}]";
            }
            input.CreatedBy = userId;

            entity = input.MapToConnector();
            _context.Connectors.Add(entity);
            _context.SaveChanges();

            return new ConnectorInsertResponse(entity);
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

        public async Task<IEnumerable<Connector>> GetAll(string userId, string? eventId = null, string? query = null, bool asUser = true)
        {
            if (!string.IsNullOrEmpty(query))
            {
                var connectors = _context.Connectors
                    .Include(item => item.Variables)
                    .Where(item => item.Name.ToLower().Contains(query));

                // do not show owner credentials
                if (asUser)
                {
                    foreach (var connector in connectors)
                    {
                        connector.Variables = connector.Variables.Where(item => item.IsPublic);
                    }
                }

                return connectors;
            }


            throw new NotImplementedException();

            //var items2 = _context.Connectors.Where(item => 
            //    connectorIds.Contains(item.Id)
            //);
            //return items2;
        }

        public async Task<IEnumerable<Connector>> GetAllBriefly(string userId)
        {
            return _context.Connectors.Where(x => x.CreatedBy == userId);
        }

        public async Task<Connector?> GetOverview(Guid connectorId)
        {
            var item = await _context.Connectors
                .Include(item => item.Variables)
                .FirstOrDefaultAsync(x => x.Id == connectorId);

            if (item == null)
            {
                throw new ApiRequestException(HttpStatusCode.NotFound, $"Did not find connector on {connectorId}");
            }

            return item;
        }

        public async Task UpdateOverview(string userId, Connector payload)
        {
            payload.CreatedBy = userId;
            var entity = _context.Connectors
                //.Include(item => item.ConnectorVariables)
                .FirstOrDefault(item => item.CreatedBy == payload.CreatedBy && item.Id == payload.Id);

            // insert
            if (entity == null)
            {
                throw new ApiRequestException(HttpStatusCode.NotFound, $"Did not find connector {payload.Id} on user:{userId}");
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
                //var preparedItem = await _helper.PrepareHttpItem(item);



                var httpItem = _context.HttpItems.FirstOrDefault(x => x.Id == item.Id);
                if (httpItem == null)
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
                throw new ApiRequestException(HttpStatusCode.NotFound, $"Missing connector: {connectorId}");
            }

            connector.Pipeline = items.ToArray();
            _context.Connectors.Update(connector);
            _context.SaveChanges();

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
                throw new ApiRequestException(HttpStatusCode.NotFound, $"Missing connector: {id} on user: {userId}");
            }

            _context.Connectors.Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
