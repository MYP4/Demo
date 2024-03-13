namespace EventPad.Api;

using EventPad.Api.Context.Seeder;
using EventPad.Api.Services.Actions;
using EventPad.Api.Services.Events;
using EventPad.Api.Services.Specific;
using EventPad.Api.Services.Tickets;
using EventPad.Logger;
using EventPad.RabbitMq;
using EventPad.Settings;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddAppLogger()
            .AddDbSeeder()
            .AddEventService()
            .AddSpecificEventService()
            .AddTicketService()
            .AddActions()
            .AddRabbitMq()
            ;

        return services;
    }
}
