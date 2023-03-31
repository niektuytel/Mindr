using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using Mindr.Api.Extensions;
using Mindr.Api.Persistence;
using Mindr.Api.Services;
using Mindr.Core.Enums;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mindr.Api.Controllers;

[Authorize]
public class ConnectorController : BaseController
{
    private readonly IConnectorClient _connectorClient;
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;

    public ConnectorController(IConnectorClient connectorClient, IMapper mapper, ApplicationContext context)
    {
        _connectorClient = connectorClient;
        _mapper = mapper;
        _context = context;
    }

    /// <remarks>
    /// Get information overview of connector
    /// </remarks>
    /// <credentials code="200">All related events</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Connector), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetOverview(Guid id)
    {
        var response = await HandleRequest(
            async () => {
                return await _connectorClient.GetOverview(id);
            }
        );

        return response;
    }

    /// <remarks>
    /// Get all connectors from user.
    /// </remarks>
    /// <credentials code="200">All related events</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Connector>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> GetAll([FromQuery] string? eventId = null, [FromQuery] string? query = null)
    {
        var response = await HandleRequest(
            async () => {
                var userId = User.GetInfo();

                if(string.IsNullOrEmpty(eventId) && string.IsNullOrEmpty(query))
                {
                    return await _connectorClient.GetAllBriefly(userId);
                }
                
                return await _connectorClient.GetAll(userId, eventId, query);
            }
        );

        return response;
    }

    /// <remarks>
    /// Insert Connector.
    /// </remarks>
    /// <credentials code="200">Successfully requested</credentials>
    /// <credentials code="400">Invalid request</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Connector), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Insert([FromBody]Connector payload)
    {
        var response = await HandleRequest(
            async () => {
                var userId = User.GetInfo();
                return await _connectorClient.Insert(userId, payload);
            }
        );

        return response;
    }

    [HttpPut]
    public IActionResult Update(Connector payload)
    {
        return Ok();
    }

    /// <remarks>
    /// Delete Connector.
    /// </remarks>
    /// <credentials code="200">Successfully requested</credentials>
    /// <credentials code="400">Invalid request</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await HandleRequest(
            async () => {
                var userId = User.GetInfo();
                await _connectorClient.Delete(userId, id);
            }
        );

        return response;
    }

}
