using Microsoft.EntityFrameworkCore;
using Mindr.Api.Migrations;
using Mindr.Api.Persistence;
using Mindr.API.Exceptions;
using Mindr.Core.Models.Connectors;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mindr.Api.Services.Connectors
{
    public class ConnectorValidator : IConnectorValidator
    {
        private readonly IApplicationContext _context;

        public ConnectorValidator(IApplicationContext context)
        {
            _context = context;
        }


        public void ThrowOnInvalidUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new API.Exceptions.HttpException(HttpStatusCode.BadRequest, $"Unknown {nameof(userId)}:'{userId}'");
            }
        }

        public void ThrowOnInvalidQuery(string? query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new HttpException(HttpStatusCode.BadRequest, $"Unknown {nameof(query)}:'{query}'");
            }
        }

        public void ThrowOnNullConnector(Guid? id, Connector? connector)
        {
            if (id == null)
            {
                throw new API.Exceptions.HttpException(HttpStatusCode.BadRequest, $"Connector id '{id}' is null");
            }

            if (connector == null)
            {
                throw new API.Exceptions.HttpException(HttpStatusCode.NotFound, $"Can't find connector on {nameof(id)}:'{id}' that is public");
            }
        }

        public async Task ThrowOnInvalidName(string userId, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new HttpException(HttpStatusCode.BadRequest, $"Unknown {nameof(name)}:'{name}'");
            }

            var entity = await _context.Connectors.FirstOrDefaultAsync(item => 
                (item.IsPublic && item.CreatedBy == userId) &&
                item.Name == name
            );
            if (entity != null)
            {
                throw new HttpException(HttpStatusCode.BadRequest, $"Connector for {{{nameof(userId)}:'{userId}',{nameof(name)}:'{name}'}} already exists");
            }
        }
    }
}