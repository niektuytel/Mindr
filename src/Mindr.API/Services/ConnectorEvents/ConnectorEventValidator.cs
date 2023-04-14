using Microsoft.Graph;
using Mindr.Api.Migrations;
using Mindr.Api.Models;
using Mindr.Api.Persistence;
using Mindr.API.Enums;
using Mindr.API.Exceptions;
using Mindr.Core.Models.Connector;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mindr.Api.Services.ConnectorEvents
{
    public class ConnectorEventValidator : IConnectorEventValidator
    {
        private readonly IApplicationContext _context;

        public ConnectorEventValidator(IApplicationContext context)
        {
            _context = context;
        }

        public void ThrowOnInvalidConnectorVariables(IEnumerable<ConnectorVariable> connectorVariables)
        {
            foreach (var variable in connectorVariables)
            {
                if (string.IsNullOrEmpty(variable.Key))
                {
                    throw new ApiRequestException(ApiResponse.BadRequest, $"Variable key of {{'{variable.Key}': '{variable.Value}'}} is invalid");
                }
                else if (string.IsNullOrEmpty(variable.Value))
                {
                    throw new ApiRequestException(ApiResponse.BadRequest, $"Variable value of {{'{variable.Key}': '{variable.Value}'}} is invalid");
                }
            }
        }

        public void ThrowOnNotUniqueConnectorVariables(IEnumerable<ConnectorVariable> connectorVariables)
        {
            foreach (var variable in connectorVariables)
            {
                var entity = _context.ConnectorVariables.FirstOrDefault(item => item.Id == variable.Id);
                if(entity != null)
                {
                    throw new ApiRequestException(ApiResponse.BadRequest, $"Connector variable of {{'id': '{variable.Id}'}} already exists");
                }
            }
        }

        public void ThrowOnInvalidEventId(string eventId)
        {
            if (string.IsNullOrEmpty(eventId))
            {
                throw new ApiRequestException(ApiResponse.BadRequest, $"Unknown {nameof(eventId)}:'{eventId}'");
            }
        }

        public void ThrowOnInvalidEventParameters(IEnumerable<EventParameter> eventParameters)
        {
            foreach (var parameter in eventParameters)
            {
                if (string.IsNullOrEmpty(parameter.Value))
                {
                    throw new ApiRequestException(ApiResponse.BadRequest, $"EventParameter value of {{'{parameter.Key}': '{parameter.Value}'}} is invalid");
                }

                // datetime validation
                if (parameter.Key == Core.Enums.EventType.OnDateTime && !DateTime.TryParse(parameter.Value, out var _))
                {
                    throw new ApiRequestException(ApiResponse.BadRequest, $"EventParameter value of {{'{parameter.Key}': '{parameter.Value}'}} is invalid on given key");
                }
            }
        }

        public void ThrowOnInvalidQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ApiRequestException(ApiResponse.BadRequest, $"Unknown {nameof(query)}:'{query}'");
            }
        }

        public void ThrowOnInvalidUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ApiRequestException(ApiResponse.BadRequest, $"Unknown {nameof(userId)}:'{userId}'");
            }
        }
        public void ThrowOnNullConnector(Guid? id, Connector? connector)
        {
            if (id == null)
            {
                throw new ApiRequestException(ApiResponse.BadRequest, $"Connector id '{id}' is null");
            }

            if (connector == null)
            {
                throw new ApiRequestException(ApiResponse.NotFound, $"Can't find connector on {nameof(id)}:'{id}' that is public");
            }
        }

        public void ThrowOnNullEvent(string userId, Guid id, ConnectorEvent? entity)
        {
            if (entity == null)
            {
                throw new ApiRequestException(ApiResponse.NotFound, $" Can't find event on {nameof(id)}:'{id}' and {nameof(userId)}:'{userId}'");
            }
        }
    }
}