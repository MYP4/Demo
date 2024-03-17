using Duende.IdentityServer.Models;

namespace EventPad.Api.Configuration;

public static class AppResources
{
    public static IEnumerable<ApiResource> Resources => new List<ApiResource>
    {
        new ApiResource("api")
    };
}
