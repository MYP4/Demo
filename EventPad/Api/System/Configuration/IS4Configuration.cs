using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using Microsoft.AspNetCore.Identity;

namespace EventPad.Api.Configuration;

public static class IS4Configuration
{
    public static IServiceCollection AddIS4(this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequiredLength = 0;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApiDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders()
            ;

        services
            .AddIdentityServer()

            .AddAspNetIdentity<User>()

            .AddInMemoryApiScopes(AppApiScopes.ApiScopes)
            .AddInMemoryClients(AppClients.Clients)
            .AddInMemoryApiResources(AppResources.Resources)
            .AddInMemoryIdentityResources(AppIdentityResources.Resources)

            //.AddTestUsers(AppApiTestUsers.ApiUsers)

            //.AddDeveloperSigningCredential()
            ;

        return services;
    }

    public static IApplicationBuilder UseIS4(this IApplicationBuilder app)
    {
        app.UseIdentityServer();

        return app;
    }
}
