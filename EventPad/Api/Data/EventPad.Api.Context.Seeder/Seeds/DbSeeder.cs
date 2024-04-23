namespace EventPad.Api.Context.Seeder;

using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using EventPad.Api.Services.Events;
using EventPad.Settings;
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

        var eventService = scope.ServiceProvider.GetService<IEventService>();
        var mainSettings = scope.ServiceProvider.GetService<MainSettings>();
        var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

        var demoHelper = new DemoHelper(userManager, eventService, settings, mainSettings);


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

        var mainSettings = scope.ServiceProvider.GetService<MainSettings>();

        var avatarName = "avatar.png";
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
            Image = avatarName,

            UserName = "Admin@adm.com",  
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        };

        await userManager.CreateAsync(admin);
        await userManager.AddPasswordAsync(admin, "123456");
    }
}
