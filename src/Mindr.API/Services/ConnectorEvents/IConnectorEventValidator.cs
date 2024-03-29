﻿using Mindr.Api.Models;
using Mindr.Domain.Models.DTO.Connector;

namespace Mindr.Api.Services.ConnectorEvents
{
    public interface IConnectorEventValidator
    {
        void ThrowOnInvalidConnectorVariables(IEnumerable<ConnectorVariable> connectorVariables);
        void ThrowOnNotUniqueConnectorVariables(IEnumerable<ConnectorVariable> connectorVariables);
        void ThrowOnInvalidEventId(string eventId);
        void ThrowOnInvalidEventParameters(IEnumerable<ConnectorEventStep> eventVariables);
        void ThrowOnInvalidQuery(string query);
        void ThrowOnInvalidUserId(string? userId);
        void ThrowOnNullConnector(Guid? id, Connector? connector);
        void ThrowOnNullEvent(string userId, Guid id, ConnectorEvent? entity);
        void ThrowOnInvalidConnectorId(Guid? connectorId);
    }
}