using Mindr.Shared.Models.Connectors;
using Mindr.HttpRunner.Models;

namespace Mindr.Api.Services.Connectors
{
    public interface IConnectorDriver
    {
        Task<IEnumerable<HttpItem>> AutoCompletePipeline(IEnumerable<ConnectorVariable> variables, IEnumerable<HttpItem> input);
        Task ProcessHttpRunnerAsync(Connector connector);
    }
}