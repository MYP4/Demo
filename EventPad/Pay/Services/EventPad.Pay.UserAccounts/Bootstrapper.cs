namespace EventPad.Pay.Services.UserAccounts;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddEventAccountService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IUserAccountService, UserAccountService>();
    }
}
