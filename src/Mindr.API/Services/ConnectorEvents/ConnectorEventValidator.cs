using Mindr.Api.Persistence;
using Mindr.Domain.Enums;
using Mindr.Domain.Models.DTO.Connector;

using System.Net;

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
                    throw new Api.Exceptions.HttpException<string>(HttpStatusCode.BadRequest, $"Variable key of {{'{variable.Key}': '{variable.Value}'}} is invalid");
                }
                else if (string.IsNullOrEmpty(variable.Value))
                {
                    throw new Api.Exceptions.HttpException<string>(HttpStatusCode.BadRequest, $"Variable value of {{'{variable.Key}': '{variable.Value}'}} is invalid");
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
                    throw new Api.Exceptions.HttpException<string>(HttpStatusCode.BadRequest, $"Connector variable of {{'id': '{variable.Id}'}} already exists");
                }
            }
        }

        public void ThrowOnInvalidEventId(string eventId)
        {
            if (string.IsNullOrEmpty(eventId))
            {
                throw new Api.Exceptions.HttpException<string>(HttpStatusCode.BadRequest, $"Unknown {nameof(eventId)}:'{eventId}'");
            }
        }

        public void ThrowOnInvalidConnectorId(Guid? connectorId)
        {
            var entity = _context.Connectors.FirstOrDefault(item => item.Id == connectorId);
            if (entity == null)
            {
                throw new Api.Exceptions.HttpException<string>(HttpStatusCode.NotFound, $"Connector of {{'id': '{connectorId}'}} not found");
            }
        }

        public void ThrowOnInvalidEventParameters(IEnumerable<ConnectorEventStep> eventParameters)
        {
            foreach (var parameter in eventParameters)
            {
                if (string.IsNullOrEmpty(parameter.Value))
                {
                    throw new Api.Exceptions.HttpException<string>(HttpStatusCode.BadRequest, $"EventParameter value of {{'{parameter.Key}': '{parameter.Value}'}} is invalid");
                }

                // datetime validation
                if (parameter.Key == EventType.OnDateTime && !DateTime.TryParse(parameter.Value, out var _))
                {
                    throw new Api.Exceptions.HttpException<string>(HttpStatusCode.BadRequest, $"EventParameter value of {{'{parameter.Key}': '{parameter.Value}'}} is invalid on given key");
                }
            }
        }

        public void ThrowOnInvalidQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new Api.Exceptions.HttpException<string>(HttpStatusCode.BadRequest, $"Unknown {nameof(query)}:'{query}'");
            }
        }

        public void ThrowOnInvalidUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Api.Exceptions.HttpException<string>(HttpStatusCode.BadRequest, $"Unknown {nameof(userId)}:'{userId}'");
            }
        }

        public void ThrowOnNullConnector(Guid? id, Connector? connector)
        {
            if (id == null)
            {
                throw new Api.Exceptions.HttpException<string>(HttpStatusCode.BadRequest, $"Connector id '{id}' is null");
            }

            if (connector == null)
            {
                throw new Api.Exceptions.HttpException<string>(HttpStatusCode.NotFound, $"Can't find connector on {nameof(id)}:'{id}' that is public");
            }
        }

        public void ThrowOnNullEvent(string userId, Guid id, ConnectorEvent? entity)
        {
            if (entity == null)
            {
                throw new Api.Exceptions.HttpException<string>(HttpStatusCode.NotFound, $" Can't find event on {nameof(id)}:'{id}' and {nameof(userId)}:'{userId}'");
            }
        }

    }
}