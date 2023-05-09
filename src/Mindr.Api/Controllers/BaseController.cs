using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Mindr.Api.Persistence;
using Mindr.Api.Exceptions;

namespace Mindr.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    /// <summary>
    /// Handles the HTTP request and returns a 200 OK response with no content.
    /// </summary>
    /// <param name="onFunction">The async function to execute.</param>
    /// <returns>A 200 OK response with no content.</returns>
    protected async Task<IActionResult> HandleRequest(Func<Task> onFunction)
    {
        try
        {
            await onFunction();
            return Ok();
        }
        catch (HttpException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
    }

    /// <summary>
    /// Handles the HTTP request and returns a 200 OK response with the specified content.
    /// </summary>
    /// <typeparam name="T">The type of the response content.</typeparam>
    /// <param name="onFunction">The async function to execute.</param>
    /// <returns>A 200 OK response with the specified content.</returns>
    protected async Task<IActionResult> HandleRequest<T>(Func<Task<T>> onFunction)
    {
        try
        {
            var response = await onFunction();
            return Ok(response);
        }
        catch (HttpException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
    }
}
