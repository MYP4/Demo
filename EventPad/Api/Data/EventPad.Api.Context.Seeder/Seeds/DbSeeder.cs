namespace EventPad.Api.Context.Seeder;

using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    }

    private static ApiDbContext DbContext(IServiceProvider serviceProvider)
    {
        return ServiceScope(serviceProvider)
            .ServiceProvider.GetRequiredService<IDbContextFactory<ApiDbContext>>().CreateDbContext();
    }

    public static void Execute(IServiceProvider serviceProvider)
    {
        Task.Run(async () =>
        {
            await AddAdministrator(serviceProvider);
            await AddDemoData(serviceProvider);
        })
            .GetAwaiter()
            .GetResult();
    }

    private static async Task AddDemoData(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null) 
            return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings.Init?.AddDemoData ?? false))
            return;


        var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
        var demoHelper = new DemoHelper(userManager);


        if ((await userManager.Users.AnyAsync(x => x.Id == demoHelper.userId1)))
            return;

        await using var context = DbContext(serviceProvider);

        await demoHelper.GenerateUsers();
        
        await demoHelper.GenerateEvents(context);

        await demoHelper.GenerateSpecifics(context);

        await context.SaveChangesAsync();
    }

    private static async Task AddAdministrator(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings.Init?.AddAdmin ?? false))
            return;

        var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

        if ((await userManager.Users.AnyAsync()))
            return;

        var adminId = Guid.Parse("5152f065-e458-4869-b4d0-4c11f72deeca");
        var admin = new User()
        {
            Id = adminId,

            FirstName = "Иван",
            SecondName = "Иванов",
            Role = UserRole.Administrator,
            Rating = 0,
            Account = adminId,
            Email = "Admin@adm.com",
            Image = "5c9cd0f4-61e3-430c-9f84-7d264a39167c_avatar.png",

            UserName = "Admin@adm.com",  
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        };

        await userManager.CreateAsync(admin);
        await userManager.AddPasswordAsync(admin, "123456");
    }
}
