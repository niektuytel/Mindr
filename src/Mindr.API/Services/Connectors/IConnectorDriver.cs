
using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Connector;

namespace Mindr.Api.Services.Connectors
{
    public interface IConnectorDriver
    {
        Task<IEnumerable<HttpItem>> AutoCompletePipeline(IEnumerable<ConnectorVariable> variables, IEnumerable<HttpItem> input);
        Task ProcessHttpRunnerAsync(Connector connector);
    }
}