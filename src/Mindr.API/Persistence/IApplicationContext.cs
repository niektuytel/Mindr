using Microsoft.EntityFrameworkCore;
using Mindr.Core.Models.ConnectorEvents;
using Mindr.Core.Models.Connectors;
using Mindr.HttpRunner.Models;

namespace Mindr.Api.Persistence
{
    public interface IApplicationContext
    {
        DbSet<Connector> Connectors { get; }
        DbSet<ConnectorVariable> ConnectorVariables { get; }
        DbSet<ConnectorEvent> ConnectorEvents { get; }
        DbSet<HttpItem> HttpItems { get; }
    }
}