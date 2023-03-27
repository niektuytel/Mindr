﻿using Mindr.Core.Models.Connector;

namespace Mindr.API.Services;

public interface IConnectorEventClient
{
    Task<IEnumerable<ConnectorEvent>> GetAll(string userId, string? eventId = null);
    Task Upsert(ConnectorEvent @event);
    Task Delete(Guid id, string userId);
}