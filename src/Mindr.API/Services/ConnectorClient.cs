using Hangfire;
using Mindr.API.Enums;
using Mindr.API.Exceptions;
using Mindr.Api.Persistence;
using Mindr.Core.Models.Connector;
using Mindr.Core.Services.Connectors;
using Microsoft.AspNetCore.Mvc;
using Mindr.API.Services;

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

        //public async Task Upsert(ConnectorEvent @event)
        //{
        //    if (@event == null)
        //    {
        //        throw new ArgumentNullException(nameof(@event));
        //    }

        //    // TODO: validate: @event

        //    var entity = _context.ConnectorEvents.FirstOrDefault(x => x.Id == @event.Id);
        //    var onInsert = entity == null;
        //    if (onInsert)
        //    {
        //        entity = @event;
        //    }
        //    else
        //    {
        //        // TODO: validate: items
        //        entity!.Update(@event);
        //    }

        //    var exists = await TryDefaultCall(entity);
        //    if (exists)
        //    {
        //        return;
        //    }

        //    exists = await TryCallOnSchedule(entity, onInsert);
        //    if (exists)
        //    {
        //        return;
        //    }

        //}

        //public async Task Delete(Guid id, string userId)
        //{
        //    var entity = _context.ConnectorEvents.FirstOrDefault(x => x.Id == id && x.UserId == userId)
        //        ?? throw new ApiRequestException(ApiResponse.NotFound, $"Connector event {id}");

        //    _context.ConnectorEvents.Remove(entity);
        //    await _context.SaveChangesAsync();

        //    // remove from background queue
        //    if (!string.IsNullOrEmpty(entity.JobId))
        //    {
        //        _backgroundJobs.Delete(entity.JobId);
        //    }
        //}




    }
}
