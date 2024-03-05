namespace EventPad.Api;

using EventPad.Logger;
using EventPad.Settings;
using EventPad.Api.Context.Seeder;
using EventPad.Api.Services.Events;
using EventPad.Api.Services.Specific;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices (this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddAppLogger()
            .AddDbSeeder()
            .AddEventService()
            .AddSpecificEventService()
            ;

        return services;
    }
}
