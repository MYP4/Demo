namespace EventPad.Pay.Services.UserAccounts;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddUserAccountService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IUserAccountService, UserAccountService>();
    }
}
