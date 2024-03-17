using Duende.IdentityServer.Models;

namespace EventPad.Api.Configuration;

public static class AppScopes
{
    public const string Admin = "admin";
    public const string User = "user";
}


public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
    new List<ApiScope>
    {
            new ApiScope(AppScopes.Admin, "Admin"),
            new ApiScope(AppScopes.User, "User")
        };
}
