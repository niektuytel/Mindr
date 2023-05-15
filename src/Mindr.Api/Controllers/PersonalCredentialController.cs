using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mindr.Api.Extensions;
using Mindr.Api.Models;
using Mindr.Api.Services.CalendarEvents;
using Mindr.Api.Services.PersonalCredentials;
using Mindr.Domain.Enums;

using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Personal;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mindr.Api.Controllers;

[Authorize]
public class PersonalCredentialController : BaseController
{
    private readonly IPersonalCredentialManager _personalCredentialManager;

    public PersonalCredentialController(IPersonalCredentialManager connectorManager)
    {
        _personalCredentialManager = connectorManager;
    }

    /// <remarks>
    /// Retrieves all personal credentials for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PersonalCredential>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get([FromQuery] Guid? id = null)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();

            if (id != null)
            {
                return await _personalCredentialManager.GetAllById(userId, (Guid)id);
            }

            return await _personalCredentialManager.GetAll(userId);
        });

        return response;
    }

    /// <remarks>
    /// Create a new personal credential for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpPost]
    [ProducesResponseType(typeof(PersonalCredential), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] PersonalCredentialDTO input)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _personalCredentialManager.Create(userId, input);
        });

        return response;
    }

    /// <remarks>
    /// Update personal credential object for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PersonalCredential), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] PersonalCredentialDTO input)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _personalCredentialManager.Update(userId, id, input);
        });

        return response;
    }

    /// <remarks>
    /// Delete personal credential by id for the authenticated user.
    /// </remarks>
    /// <credentials code="200">Success</credentials>
    /// <credentials code="400">Invalid credentials</credentials>
    /// <credentials code="401">Unauthorized</credentials>
    /// <credentials code="404">Not Found</credentials>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(PersonalCredential), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ErrorMessageResponse), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await HandleRequest(async () => {
            var userId = User.GetUserId();
            return await _personalCredentialManager.Delete(userId, id);
        });

        return response;
    }

}
