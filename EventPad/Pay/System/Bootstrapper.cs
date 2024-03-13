namespace EventPad.Pay;

using EventPad.Logger;
using EventPad.Settings;
using EventPad.RabbitMq;
using EventPad.Api.Services.Actions;
using EventPad.Pay.Services.EventAccounts;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddAppLogger()
            .AddEventAccountService()
            .AddActions()
            .AddRabbitMq()
            ;

        services.AddSingleton<ITaskExecutor, TaskExecutor>();

        return services;
    }
}
