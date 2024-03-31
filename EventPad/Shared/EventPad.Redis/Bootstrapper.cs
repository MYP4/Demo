using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EventPad.Common;

namespace EventPad.Redis;


public static class Bootstrapper
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Settings.Load<RedisSettings>("Cache", configuration);
        services.AddSingleton(settings);

        services.AddSingleton<IRedisService, RedisService>();

        return services;
    }
}
