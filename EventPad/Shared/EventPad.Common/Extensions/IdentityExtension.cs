namespace EventPad.Common.Extensions;

using EventPad.Common;
using System;
using System.Linq;
using System.Security.Claims;

public static class IdentityExtensionMethods
{
    public static Guid GetUserGuid(this ClaimsPrincipal principal)
    {
        var id = principal.Claims.SingleOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value ??
                 principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(id, out var userGuid)) return userGuid;
        throw new ProcessException("User not found.");
    }
}