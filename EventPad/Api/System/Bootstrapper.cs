﻿namespace EventPad.Api;

using EventPad.Api.Configuration;
using EventPad.Api.Context.Seeder;
using EventPad.Api.Services.Events;
using EventPad.Api.Services.Specific;
using EventPad.Api.Services.Tickets;
using EventPad.Api.Services.Users;
using EventPad.Logger;
using EventPad.RabbitMq;
using EventPad.Services.Actions;
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
            .AddUserService()
            .AddActions()
            .AddRabbitMq()
            ;

        return services;
    }
}
