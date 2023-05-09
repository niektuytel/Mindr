using Mindr.Api.Persistence;

using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.HttpRunner.Services;
using Mindr.Domain.Models.DTO.Connector;

namespace Mindr.Api.Services.Connectors
{
    public class ConnectorDriver: IConnectorDriver
    {
        private readonly IHttpRunnerClient _httpRunnerClient;
        private readonly ApplicationContext _context;

        public ConnectorDriver(IHttpRunnerClient httpRunnerClient, ApplicationContext context)
        {
            _httpRunnerClient = httpRunnerClient;
            _context = context;
        }

        public async Task<IEnumerable<HttpItem>> AutoCompletePipeline(IEnumerable<ConnectorVariable> variables, IEnumerable<HttpItem> input)
        {
            // TODO: AutoComplete missing fields

            return input;
        }

        public async Task ProcessHttpRunnerAsync(Connector connector)
        {
            var pipeline = connector.Pipeline.ToList();
            pipeline = await _httpRunnerClient.SendAsync(pipeline);

            // TODO: safe current outputted responses
            //throw new NotImplementedException();

        }



    }
}