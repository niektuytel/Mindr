using Hangfire;
using Microsoft.EntityFrameworkCore;
using Mindr.Api.Persistence;
using Mindr.Domain.Models.DTO.Connector;

using Mindr.Domain.HttpRunner.Services;
using System.Net;
using Mindr.Domain.Enums;
using Mindr.Api.Services.Connectors;

namespace Mindr.Api.Services.CalendarEvents
{
    public class CalendarEventDriver : ICalendarEventDriver
    {
        private readonly IConnectorDriver _connectorDriver;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly ApplicationContext _context;

        public CalendarEventDriver(IConnectorDriver connectorDriver, IBackgroundJobClient backgroundJobs, ApplicationContext context)
        {
            _connectorDriver = connectorDriver;
            _backgroundJobs = backgroundJobs;
            _context = context;
        }

        //public async Task<string?> SyncCalendar(ConnectorEvent entity)
        //{
        //    var jobId = await EnqueueConnectorEventAsync(entity);
        //    if (!string.IsNullOrEmpty(jobId))
        //    {
        //        return jobId;
        //    }

        //    jobId = await ScheduleConnectorEventAsync(entity);
        //    if (!string.IsNullOrEmpty(jobId))
        //    {
        //        return jobId;
        //    }

        //    return null;
        //}

    }
}
