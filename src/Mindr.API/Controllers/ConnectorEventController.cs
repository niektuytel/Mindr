using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.OpenApi.Expressions;
using Mindr.Api.Extensions;
using Mindr.Api.Persistence;
using Mindr.API.Exceptions;
using Mindr.API.Services;
using Mindr.Core.Models.Connector;

namespace Mindr.Api.Controllers;

[Authorize]
public class ConnectorEventController : BaseController
{
    private readonly IConnectorEventClient _connectorEventClient;

    public ConnectorEventController(IConnectorEventClient connectorEventClient)
    {
        _connectorEventClient = connectorEventClient;
    }

    /// <remarks>
    /// Get all connector events from user.
    /// </remarks>
    /// <credentials code="200">All related events</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ConnectorEvent>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> GetAll()
    {
        var response = await HandleRequest(
            async () => {
                var userId = User.GetInfo();

                return await _connectorEventClient.GetAll(userId);
            }
        );

        return response;
    }

    /// <remarks>
    /// Create Connector Event.
    /// </remarks>
    /// <credentials code="200">Successfully requested</credentials>
    /// <credentials code="400">Invalid request</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Create([FromBody] ConnectorEvent payload)
    {
        var response = await HandleRequest(
            async () => {
                var userId = User.GetInfo();
                payload.UserId = userId;

                await _connectorEventClient.Create(payload);
            }
        );

        return response;
    }

    /// <remarks>
    /// Update Connector Event.
    /// </remarks>
    /// <credentials code="200">Successfully requested</credentials>
    /// <credentials code="400">Invalid request</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Update(Guid id, [FromBody]ConnectorEvent payload)
    {
        var response = await HandleRequest(
            async () => {
                var userId = User.GetInfo();
                payload.Id = id;
                payload.UserId = userId;

                await _connectorEventClient.Update(payload);
            }
        );

        return response;
    }

    /// <remarks>
    /// Delete connector event.
    /// </remarks>
    /// <credentials code="200">Successfull delete connector</credentials>
    /// <credentials code="400">Bad request</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await HandleRequest(
            async () => {
                var userId = User.GetInfo();
                await _connectorEventClient.Delete(id, userId);
            }
        );

        return response;
    }

}
