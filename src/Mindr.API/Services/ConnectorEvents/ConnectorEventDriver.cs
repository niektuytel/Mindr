using Hangfire;
using Microsoft.EntityFrameworkCore;
using Mindr.Api.Persistence;
using Mindr.Api.Services.Connectors;
using Mindr.Core.Models.ConnectorEvents;
using Mindr.Core.Models.Connectors;
using Mindr.HttpRunner.Services;
using System.Net;

namespace Mindr.Api.Services.ConnectorEvents
{
    public class ConnectorEventDriver : IConnectorEventDriver
    {
        private readonly IConnectorDriver _connectorDriver;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly ApplicationContext _context;

        public ConnectorEventDriver(IConnectorDriver connectorDriver, IBackgroundJobClient backgroundJobs, ApplicationContext context)
        {
            _connectorDriver = connectorDriver;
            _backgroundJobs = backgroundJobs;
            _context = context;
        }

        public async Task<string?> ProcessConnectorEventAsync(ConnectorEvent entity)
        {
            var jobId = await EnqueueConnectorEventAsync(entity);
            if (!string.IsNullOrEmpty(jobId))
            {
                return jobId;
            }

            jobId = await ScheduleConnectorEventAsync(entity);
            if (!string.IsNullOrEmpty(jobId))
            {
                return jobId;
            }

            return null;
        }

        private async Task<string?> EnqueueConnectorEventAsync(ConnectorEvent entity)
        {
            if (entity.EventParameters.Any())
            {
                return null;
            }

            var connector = await GetConnectorAsync(entity.ConnectorId);
            connector.Variables = entity.ConnectorVariables;

            var jobId = _backgroundJobs.Enqueue(() => _connectorDriver.ProcessHttpRunnerAsync(connector));

            return jobId;
        }

        private async Task<string?> ScheduleConnectorEventAsync(ConnectorEvent entity)
        {
            var schedule = entity.EventParameters.FirstOrDefault(item => item.Key == Core.Enums.EventType.OnDateTime)?.Value;
            if (string.IsNullOrEmpty(schedule) || !DateTime.TryParse(schedule, out var datetime))
            {
                return null;
            }
            else if (datetime < DateTime.Now)
            {
                throw new API.Exceptions.HttpException(HttpStatusCode.BadRequest, "Scheduled datetime must be in the future.");
            }

            var connector = await GetConnectorAsync(entity.ConnectorId);
            connector.Variables = entity.ConnectorVariables;

            if (!string.IsNullOrEmpty(entity.JobId))
            {
                _backgroundJobs.Delete(entity.JobId);
            }

            var jobid = _backgroundJobs.Schedule(() => _connectorDriver.ProcessHttpRunnerAsync(connector), datetime);

            return jobid;
        }

        private async Task<Connector> GetConnectorAsync(Guid? connectorId)
        {
            var connector = await _context.Connectors
                .Include(item => item.Variables)
                .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Variables)
                .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Url)
                .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Header)
                .Include(item => item.Pipeline).ThenInclude(item => item.Request).ThenInclude(item => item.Body).ThenInclude(item => item.Options).ThenInclude(item => item.Raw)
                .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.Variables)
                .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Variables)
                .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Url).ThenInclude(item => item.Query)
                .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Header)
                .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.OriginalRequest).ThenInclude(item => item.Body).ThenInclude(item => item.Options).ThenInclude(item => item.Raw)
                .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.Header)
                .Include(item => item.Pipeline).ThenInclude(item => item.Response).ThenInclude(item => item.Cookie)
                .FirstOrDefaultAsync(item => item.Id == connectorId);

            if (connector == null)
            {
                throw new API.Exceptions.HttpException(HttpStatusCode.BadRequest, $"connector on id: '{connectorId}' is unknown");
            }

            return connector;
        }

    }
}
