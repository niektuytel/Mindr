using Microsoft.Graph;
using Microsoft.Identity.Web;
using Mindr.API.Exceptions;
using System.Net;
using System.Security.Claims;

namespace Mindr.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetInfo(this ClaimsPrincipal claims)
    {
        // Validate Input
        var identity = claims.Identities.FirstOrDefault();
        if (identity == null)
        {
            throw new ApiRequestException(HttpStatusCode.BadRequest, "No User Identity found on given Bearer token");
        }

        //var tenantId = claims.GetTenantId();
        //if (string.IsNullOrEmpty(tenantId))
        //{
        //    throw new ApiRequestException(HttpStatusCode.BadRequest, "Missing tenant id on Bearer token");
        //}

        var objectId = claims.GetObjectId();
        if (string.IsNullOrEmpty(objectId))
        {
            throw new ApiRequestException(HttpStatusCode.BadRequest, "Missing user id on Bearer token");
        }

        //var emailClaim = claims.FindFirst(ClaimTypes.Email);
        //if (emailClaim == null || string.IsNullOrEmpty(emailClaim.Value))
        //{
        //    throw new ApiRequestException(HttpStatusCode.BadRequest, "Missing email on Bearer token");
        //}

        return objectId;
    }
}
