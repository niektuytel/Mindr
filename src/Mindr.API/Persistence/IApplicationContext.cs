using Microsoft.EntityFrameworkCore;
using Mindr.Core.Models.Connector;

namespace Mindr.Api.Persistence
{
    public interface IApplicationContext
    {
        DbSet<ConnectorEvent> ConnectorEvents { get; }
        DbSet<ConnectorVariable> ConnectorVariables { get; }
    }
}