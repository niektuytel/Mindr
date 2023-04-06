using Microsoft.EntityFrameworkCore;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;

namespace Mindr.Api.Persistence
{
    public interface IApplicationContext
    {
        DbSet<ConnectorEvent> ConnectorEvents { get; }
        DbSet<ConnectorVariable> ConnectorVariables { get; }
        DbSet<HttpItem> HttpItems { get; }
    }
}