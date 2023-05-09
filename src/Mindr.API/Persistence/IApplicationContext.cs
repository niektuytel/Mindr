using Microsoft.EntityFrameworkCore;
using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Connector;

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