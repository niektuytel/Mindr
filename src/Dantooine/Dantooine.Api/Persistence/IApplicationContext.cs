using Microsoft.EntityFrameworkCore;
using Mindr.Shared.Models.ConnectorEvents;
using Mindr.Shared.Models.Connectors;
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