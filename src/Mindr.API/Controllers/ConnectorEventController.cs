using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mindr.Api.Extensions;
using Mindr.Api.Models;
using Mindr.Api.Models.ConnectorEvents;
using Mindr.Api.Services.ConnectorEvents;
using Mindr.Domain.Models.DTO.Connector;
using System.Net;

namespace Mindr.Api.Controllers;

[Authorize]
public class ConnectorEventController : BaseController
{
    private readonly IConnectorEventManager _connectorEventManager;

    public ConnectorEventController(IConnectorEventManager connectorEventHandler)
    {
        _connectorEventManager = connectorEventHandler;
    }

    /// <remarks>
    /// Retrieves all connector events for the authenticated user, optionally filtered by event ID or query string.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpGet("personal")]
    [ProducesResponseType(typeof(IEnumerable<ConnectorEvent>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll([FromQuery] string? eventId = null, [FromQuery] string? query = null)
    {
        var response = await HandleRequest(
            async () => {
                var userId = User.GetUserId();

                if (!string.IsNullOrEmpty(eventId))
                {
                    return await _connectorEventManager.GetAllByEventId(userId, eventId);
                }
                else if (!string.IsNullOrEmpty(query))
                {
                    return await _connectorEventManager.GetAllByQuery(userId, query);
                }

                return await _connectorEventManager.GetAll(userId);
            }
        );

        return response;
    }

    /// <remarks>
    /// Retrieves a specific connector event by ID for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not found</credentials>
    [HttpGet("personal/{id}")]
    [ProducesResponseType(typeof(IEnumerable<ConnectorEvent>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById([FromRoute]Guid id)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _connectorEventManager.GetById(userId, id);
        });

        return response;
    }

    /// <remarks>
    /// Creates a new connector event for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not found</credentials>
    [HttpPost("personal")]
    [ProducesResponseType(typeof(ConnectorEvent), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Create([FromBody] ConnectorEventOnCreate input)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _connectorEventManager.Create(userId, input);
        });

        return response;
    }

    /// <remarks>
    /// Updates a specific connector event by ID for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid request</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpPut("personal/{id}")]
    [ProducesResponseType(typeof(ConnectorEvent), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateById([FromRoute]Guid id, [FromBody]ConnectorEventOnUpdate input)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _connectorEventManager.UpdateById(userId, id, input);
        });

        return response;
    }

    /// <remarks>
    /// Deletes a specific connector event by ID for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Bad request</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpDelete("personal/{id}")]
    [ProducesResponseType(typeof(ConnectorEvent), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteById([FromRoute]Guid id)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _connectorEventManager.DeleteById(userId, id);
        });

        return response;
    }

}
