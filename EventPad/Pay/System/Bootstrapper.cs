namespace EventPad.Pay;

using EventPad.Logger;
using EventPad.Settings;
using EventPad.RabbitMq;
using EventPad.Services.Actions;
using EventPad.Pay.Services.EventAccounts;
using EventPad.Pay.Services.UserAccounts;
using EventPad.Pay.Services.Transactions;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddLogSettings()
            .AddAppLogger()
            .AddActions()
            .AddRabbitMq()
            .AddUserAccountService()
            .AddEventAccountService()
            //.AddTransactionService()
            ;

        services.AddSingleton<ITaskExecutor, TaskExecutor>();

        return services;
    }
}
