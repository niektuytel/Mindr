using Mindr.Api.Models;
using Mindr.Core.Models.ConnectorEvents;
using Mindr.Core.Models.Connectors;

namespace Mindr.Api.Services.ConnectorEvents
{
    public interface IConnectorEventValidator
    {
        void ThrowOnInvalidConnectorVariables(IEnumerable<ConnectorVariable> connectorVariables);
        void ThrowOnNotUniqueConnectorVariables(IEnumerable<ConnectorVariable> connectorVariables);
        void ThrowOnInvalidEventId(string eventId);
        void ThrowOnInvalidEventParameters(IEnumerable<ConnectorEventParameter> eventParameters);
        void ThrowOnInvalidQuery(string query);
        void ThrowOnInvalidUserId(string? userId);
        void ThrowOnNullConnector(Guid? id, Connector? connector);
        void ThrowOnNullEvent(string userId, Guid id, ConnectorEvent? entity);
        void ThrowOnInvalidConnectorId(Guid? connectorId);
    }
}