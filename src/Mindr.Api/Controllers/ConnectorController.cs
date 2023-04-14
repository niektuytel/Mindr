using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using Mindr.Api.Extensions;
using Mindr.Api.Models;
using Mindr.Api.Persistence;
using Mindr.Api.Services.Connectors;

using Mindr.Core.Enums;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using System.Net;
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
    /// CreatePersonalEvent Connector.
    /// </remarks>
    /// <credentials code="200">Successfully requested</credentials>
    /// <credentials code="400">Invalid request</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ConnectorInsertResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Models.ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Insert([FromBody] ConnectorInsert payload)
    {
        var response = await HandleRequest(
            async () => {
                var userId = User.GetInfo();
                return await _connectorClient.Create(userId, payload);
            }
        );

        return response;
    }






    // TODO: need Prepare & Validation functions



    /// <remarks>
    /// Get pipeline of connector
    /// </remarks>
    /// <credentials code="200">All related events</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<HttpItem>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await HandleRequest(
            async () => {
                return await _connectorClient.Get(id);
            }
        );

        return response;
    }

    /// <remarks>
    /// Get all connectors.
    /// </remarks>
    /// <credentials code="200">All related connectors</credentials>
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

                if (string.IsNullOrEmpty(query) == false)
                {
                    return await _connectorClient.GetAll(userId, eventId, query, asUser: true);
                }

                return await _connectorClient.GetAllBriefly(userId);
            }
        );

        return response;
    }

    /// <remarks>
    /// Get overview of connector
    /// </remarks>
    /// <credentials code="200">All related events</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpGet("{id}/overview")]
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
    /// Update Overview Connector.
    /// </remarks>
    /// <credentials code="200">Successfully requested</credentials>
    /// <credentials code="400">Invalid request</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpPut("{id}/overview")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> UpdateOverview(Guid id, [FromBody] Connector payload)
    {
        var response = await HandleRequest(
            async () => {
                var userId = User.GetInfo();
                await _connectorClient.UpdateOverview(userId, payload);
            }
        );

        return response;
    }

    /// <remarks>
    /// Update HttpItems Connector.
    /// </remarks>
    /// <credentials code="200">Successfully requested</credentials>
    /// <credentials code="400">Invalid request</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpPut("{id}/httpItems")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> UpdateHttpItems(Guid id, [FromBody] IEnumerable<HttpItem> payload)
    {
        var response = await HandleRequest(
            async () => {
                var userId = User.GetInfo();
                await _connectorClient.UpdateHttpItems(userId, id, payload);
            }
        );

        return response;
    }

    /// <remarks>
    /// DeletePersonalEventById Connector.
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
