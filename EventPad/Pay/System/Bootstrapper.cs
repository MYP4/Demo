namespace EventPad.Pay;

using EventPad.Logger;
using EventPad.Settings;
using EventPad.RabbitMq;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddAppLogger()
            .AddRabbitMq()
            ;

        return services;
    }
}
