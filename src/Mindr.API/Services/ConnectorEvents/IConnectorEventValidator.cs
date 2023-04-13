using Mindr.Api.Models;
using Mindr.Core.Models.Connector;

namespace Mindr.Api.Services.ConnectorEvents
{
    public interface IConnectorEventValidator
    {
        void ThrowOnInvalidConnectorVariables(IEnumerable<ConnectorVariable> connectorVariables);
        void ThrowOnInvalidEventId(string eventId);
        void ThrowOnInvalidEventParameters(IEnumerable<EventParameter> eventParameters);
        void ThrowOnInvalidQuery(string query);
        void ThrowOnInvalidUserId(string userId);
        void ThrowOnNullConnector(Guid? id, Connector? connector);
        void ThrowOnNullEvent(string userId, Guid id, ConnectorEvent? entity);
    }
}