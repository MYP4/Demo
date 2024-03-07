using Microsoft.Extensions.DependencyInjection;

namespace EventPad.Api.Services.EventAccounts;
public static class Bootstrapper
{
    public static IServiceCollection AddEventAccountService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IEventAccountService, EventAccountService>();
    }
}
