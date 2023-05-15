using Microsoft.EntityFrameworkCore;
using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.Personal;

namespace Mindr.Api.Persistence
{
    public interface IApplicationContext
    {
        DbSet<PersonalCalendar> PersonalCalendars { get; }
        DbSet<PersonalCredential> PersonalCredentials { get; }
        DbSet<Connector> Connectors { get; }
        DbSet<ConnectorVariable> ConnectorVariables { get; }
        DbSet<ConnectorEvent> ConnectorEvents { get; }
        DbSet<HttpItem> HttpItems { get; }
    }
}