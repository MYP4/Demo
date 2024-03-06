namespace EventPad.Pay;

using EventPad.Logger;
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
            ;

        return services;
    }
}
