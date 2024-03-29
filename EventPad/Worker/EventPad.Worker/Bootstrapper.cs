namespace EventPad.Worker;

using EventPad.EmailService;
using EventPad.Logger;
using EventPad.RabbitMq;
using EventPad.Services.Actions;
using EventPad.Settings;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddLogSettings()
            .AddAppLogger()
            .AddActions()
            .AddRabbitMq()
            .AddEmailSettings()
            .AddEmailSender()
            ;

        services.AddSingleton<ITaskExecutor, TaskExecutor>();

        return services;
    }
}
