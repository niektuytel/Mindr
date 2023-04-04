﻿using Mindr.Core.Models.Connector;

namespace Mindr.Api.Services
{
    public interface IConnectorClient
    {
        Task<Connector?> GetById(Guid id);
        Task<Connector?> GetOverview(Guid connectorId);



        Task UpdateOverview(string userId, Connector payload);
        Task<IEnumerable<Connector>> GetAll(string userId, string? eventId, string? query);
        Task<IEnumerable<Connector>> GetAllBriefly(string userId);
        Task<Connector> Insert(string userId, Connector payload);
        Task Delete(string userId, Guid id);
    }
}