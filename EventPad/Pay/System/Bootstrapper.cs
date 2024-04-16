namespace EventPad.Pay;

using EventPad.Logger;
using EventPad.Settings;
using EventPad.RabbitMq;
using EventPad.Services.Actions;
using EventPad.Pay.Services.EventAccounts;
using EventPad.Pay.Services.UserAccounts;
using EventPad.Pay.Services.Transactions;
using EventPad.Redis;
using EventPad.Pay.Context.Seeder;

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
            .AddActions()
            .AddRabbitMq()
            .AddUserAccountService()
            .AddEventAccountService()
            .AddTransactionService()
            .AddRedis()
            ;

        services.AddSingleton<ITaskExecutor, TaskExecutor>();

        return services;
    }
}
