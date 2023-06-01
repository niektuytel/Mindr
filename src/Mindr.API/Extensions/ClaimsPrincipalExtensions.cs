using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Mindr.Api.Exceptions;
using System;
using System.Net;
using System.Security.Claims;

namespace Mindr.Api.Extensions;

/// <summary>
/// Provides extension methods for ClaimsPrincipal.
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Gets the user ID from the claims principal.
    /// </summary>
    /// <param name="claims">The claims principal.</param>
    /// <returns>The user ID.</returns>
    /// <exception cref="HttpException">Thrown when no user identity is found on the given bearer token or when the user ID is missing from the bearer token.</exception>
    public static string GetUserId(this ClaimsPrincipal claims)
    {
        var identity = claims.Identities.FirstOrDefault();
        if (identity == null)
        {
            throw new HttpException<string>(HttpStatusCode.BadRequest, "No user identity found on given bearer token");
        }

        // Get the "sub" claim value from the current user
        var sub = claims.FindFirstValue("sub");
        if (!string.IsNullOrEmpty(sub))
        {
            return sub;
        }

        var userId = claims.GetObjectId();
        if (!string.IsNullOrEmpty(userId))
        {
            return userId;
        }

        throw new HttpException<string>(HttpStatusCode.BadRequest, "Missing user unique identifier on bearer token");

    }
}
