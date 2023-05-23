using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mindr.Api.Extensions;
using Mindr.Api.Models;
using Mindr.Api.Models.Connectors;
using Mindr.Api.Services.Connectors;
using Mindr.Api.Services.PersonalCredentials;
using Mindr.Domain.Enums;

using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Connector;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mindr.Api.Controllers;

[Authorize]
public class ConnectorController : BaseController
{
    private readonly IConnectorManager _connectorManager;

    public ConnectorController(IConnectorManager connectorManager)
    {
        _connectorManager = connectorManager;
    }

    /// <remarks>
    /// Retrieves all connectors for the authenticated user, optionally filtered by event ID or query string.
    /// Retreive on query is not dependend on user id. It is a search over all available conenctors.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ConnectorBriefDTO>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll([FromQuery] string? eventId = null, [FromQuery] string? query = null)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            if (!string.IsNullOrEmpty(eventId))
            {
                return await _connectorManager.GetAllByEventId(userId, eventId);
            }
            else if (!string.IsNullOrEmpty(query))
            {
                return await _connectorManager.GetAllByQuery(userId, query);
            }

            return await _connectorManager.GetAll(userId);
        });

        return response;
    }

    /// <remarks>
    /// Creates a new connector.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpPost("personal")]
    [ProducesResponseType(typeof(Connector), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] ConnectorOnCreate input)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _connectorManager.Create(userId, input);
        });

        return response;
    }

    /// <remarks>
    /// Retrieves a specific connector by ID for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpGet("personal/{id}")]
    [ProducesResponseType(typeof(Connector), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById([FromRoute]Guid id)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _connectorManager.GetById(userId, id);
        });

        return response;
    }

    /// <remarks>
    /// Update connector overview of a specific connector by ID of the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpGet("personal/{id}/overview")]
    [ProducesResponseType(typeof(ConnectorOverviewDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetOverview(Guid id)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _connectorManager.GetOverview(userId, id);
        });

        return response;
    }

    /// <remarks>
    /// Update Overview Connector.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpPut("personal/{id}/overview")]
    [ProducesResponseType(typeof(ConnectorOverviewDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateOverview(Guid id, [FromBody] ConnectorOverviewDTO input)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _connectorManager.UpdateOverview(userId, id, input);
        });

        return response;
    }

    /// <remarks>
    /// Update HttpItems Connector.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpPut("personal/{id}/pipeline")]
    [ProducesResponseType(typeof(IEnumerable<HttpItem>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateHttpItems(Guid id, [FromBody] IEnumerable<HttpItem> input)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _connectorManager.UpdateHttpItems(userId, id, input);
        });

        return response;
    }

    /// <remarks>
    /// Delete Connector by id.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpDelete("personal/{id}")]
    [ProducesResponseType(typeof(Connector), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _connectorManager.Delete(userId, id);
        });

        return response;
    }

}
