using Mindr.Shared.Models.Connectors;

namespace Mindr.Api.Services.Connectors
{
    public interface IConnectorValidator
    {
        Task ThrowOnInvalidName(string userId, string name);
        void ThrowOnInvalidQuery(string? query);
        void ThrowOnInvalidUserId(string userId);
        void ThrowOnNullConnector(Guid? id, Connector? entity);
    }
}