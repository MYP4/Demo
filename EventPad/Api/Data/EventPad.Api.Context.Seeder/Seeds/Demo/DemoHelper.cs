namespace EventPad.Api.Context.Seeder;

using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Api.Services.Events;
using EventPad.Common.Files;
using EventPad.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

public class DemoHelper
{
    public Guid userId1 = Guid.Parse("142c4915-c4b3-4254-b0b2-8f0c34960c6e");
    public Guid userId2 = Guid.Parse("29252196-f44e-49e0-b69f-8a08ec21d27b");
    public Guid userId3 = Guid.Parse("43ba55b1-d0e9-44fc-aa7a-55263e8c721d");

    public Guid specificId1 = Guid.Parse("a09fdf29-ac10-48cb-b408-96b4ea8d1809");
    public Guid specificId2 = Guid.Parse("4b7d1d7e-75a8-4761-98e7-d859c97e4be6");
    public Guid specificId3 = Guid.Parse("cc15c23a-f10c-44d5-9224-55085243075b");
    public Guid specificId4 = Guid.Parse("756009f5-d38d-4e71-951d-2eaa5c885c8a");
    public Guid specificId5 = Guid.Parse("70cc5113-2558-4c2d-a38a-fb69e84edaff");
    public Guid specificId6 = Guid.Parse("841ca45e-ac56-4cc2-bb82-c6983263dced");

    private readonly UserManager<User> userManager;
    private readonly IEventService eventService;
    private readonly DbSettings dbSettings;
    private readonly MainSettings mainSettings;
    private List<User> users = new List<User>();
    private List<Event> events = new List<Event>();
    private List<SpecificEvent> specifics = new List<SpecificEvent>();

    public DemoHelper(UserManager<User> userManager, IEventService eventService, DbSettings dbSettings, MainSettings mainSettings)
    {
        this.userManager = userManager;
        this.eventService = eventService;
        this.dbSettings = dbSettings;
        this.mainSettings = mainSettings;
    }

    public async Task GenerateUsers()
    {
        var avatarName = "avatar.png";
        var source = Path.Combine(dbSettings.Init.DemoFiles, avatarName);
        var target = Path.Combine(mainSettings.RootDir, mainSettings.FileDir, avatarName);
        File.Copy(source, target);


        var user1 = new User()
        {
            Id = userId1,
            FirstName = "Василий",
            SecondName = "Петров",
            Role = UserRole.Regular,
            Rating = 3,
            Account = userId1,
            Image = avatarName,
            Email = "Petrov@pad.com",

            UserName = "Petrov@pad.com",
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
        };
        await userManager.CreateAsync(user1);
        await userManager.AddPasswordAsync(user1, "qwertyui");
        users.Add(user1);


        var user2 = new User()
        {
            Id = userId2,
            FirstName = "Ольга",
            SecondName = "Анисимова",
            Role = UserRole.Regular,
            Rating = 3,
            Account = userId2,
            Image = avatarName,
            Email = "a.olga@pad.com",

            UserName = "a.olga@pad.com",
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
        };
        await userManager.CreateAsync(user2);
        await userManager.AddPasswordAsync(user2, "12345678");
        users.Add(user2);


        var user3 = new User()
        {
            Id = userId3,
            FirstName = "Максим",
            SecondName = "Гусев",
            Role = UserRole.Regular,
            Rating = 3,
            Account = userId3,
            Image = avatarName,
            Email = "GusevMaks@pad.com",

            UserName = "GusevMaks@pad.com",
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
        };
        await userManager.CreateAsync(user3);
        await userManager.AddPasswordAsync(user3, "MaksGus");
        users.Add(user3);
    }

    public async Task GenerateEvents(ApiDbContext context)
    {
        var events = new List<CreateEventModel>
        {
            new CreateEventModel()
            {
                Name = "Волейбол",
                Description = "2 часа",
                Price = 150,
                Address = "ВГУ",
                Type = EventType.Multiple,
                Image = new FileData() {Name = "volleyball.jpg"},
                AdminId = users[0].Id
            },
            new CreateEventModel()
            {
                Name = "Баскетбол",
                Description = "1 час",
                Price = 100,
                Address = "ВГАУ",
                Type = EventType.Single,
                Image = new FileData() {Name = "basketball.jpg"},
                AdminId = users[0].Id
            },
            new CreateEventModel()
            {
                Name = "Футбол",
                Description = "2 часа",
                Price = 200,
                Address = "ВГУ",
                Type = EventType.Single,
                Image = new FileData() {Name = "football.jpg"},
                AdminId = users[2].Id
            },
            new CreateEventModel()
            {
                Name = "Футбол",
                Description = "3 часа",
                Price = 300,
                Address = "ВГТУ",
                Type = EventType.Single,
                Image = new FileData() {Name = "football.jpg"},
                AdminId = users[2].Id
            },
            new CreateEventModel()
            {
                Name = "Баскетбол",
                Description = "2 часа",
                Price = 200,
                Address = "ВГАУ",
                Type = EventType.Single,
                Image = new FileData() {Name = "basketball.jpg"},
                AdminId = users[2].Id
            }
        };

        foreach (var _event in events)
        {
            var source = Path.Combine(dbSettings.Init.DemoFiles, _event.Image.Name);

            var content = File.ReadAllBytes(source);

            _event.Image = new FileData() { Name = _event.Image.Name, Extension = ".jpg", Content = content};

            var model = await eventService.Create(_event);

            var eventEntity = await context.Events.FirstOrDefaultAsync(x => x.Uid == model.Id);

            this.events.Add(eventEntity);
        }
        
    }

    public async Task GenerateSpecifics(ApiDbContext context)
    {
        var specifics = new List<SpecificEvent>
        {
            new SpecificEvent
            {
                Id = 1,
                Uid = specificId1,
                EventId = events[0].Id,
                Description = "",
                TicketCount = 14,
                Price = 200,
                Address = "Главный корпус ВГУ, 3 этаж, спортзал",
                Date = new DateOnly(2024, 5, 4),
                DayOfWeek = DayOfWeek.Saturday,
                Time = new TimeOnly(19, 00, 00),
                Private = true,
                Code = "b5540124-3cee-474b-8c95-9ca8e0c06877".ToUpper().Replace("-", ""),
                Rating = 4
            },
            new SpecificEvent
            {
                Id = 2,
                Uid = specificId2,
                EventId = events[0].Id,
                Description = "",
                TicketCount = 21,
                Price = 200,
                Address = "ЖурФак ВГУ, 1 этаж, спортзал",
                Date = new DateOnly(2024, 5, 1),
                DayOfWeek = DayOfWeek.Wednesday,
                Time = new TimeOnly(10, 30, 00),
                Private = true,
                Code = "5f2a74a1-0c24-4bf1-8ed4-632deb170aa3".ToUpper().Replace("-", ""),
                Rating = 4
            },            new SpecificEvent
            {
                Id = 3,
                Uid = specificId3,
                EventId = events[1].Id,
                Description = "",
                TicketCount = 15,
                Price = 300,
                Address = "Главный корпус ВГАУ, 3 этаж, спортзал",
                Date = new DateOnly(2024, 5, 6),
                DayOfWeek = DayOfWeek.Monday,
                Time = new TimeOnly(19, 00, 00),
                Private = true,
                Code = "23240436-bc5f-490b-95c2-ada1b551aa4e".ToUpper().Replace("-", ""),
                Rating = 4
            },
            new SpecificEvent
            {
                Id = 4,
                Uid = specificId4,
                EventId = events[2].Id,
                Description = "",
                TicketCount = 14,
                Price = 200,
                Address = "ЖурФак ВГУ, 1 этаж, спортзал",
                Date = new DateOnly(2024, 5, 5),
                DayOfWeek = DayOfWeek.Sunday,
                Time = new TimeOnly(19, 00, 00),
                Private = true,
                Code = "e3f2e9c1-63ac-417c-8a19-b911fc9a9018".ToUpper().Replace("-", ""),
                Rating = 4
            },
            new SpecificEvent
            {
                Id = 5,
                Uid = specificId5,
                EventId = events[3].Id,
                Description = "",
                TicketCount = 12,
                Price = 200,
                Address = "1 Корпус ВГТУ, 2 этаж, спортзал",
                Date = new DateOnly(2024, 5, 5),
                DayOfWeek = DayOfWeek.Sunday,
                Time = new TimeOnly(19, 00, 00),
                Private = true,
                Code = "305366ce-6239-4857-acb2-b9c0982928b7".ToUpper().Replace("-", ""),
                Rating = 4
            },
            new SpecificEvent
            {
                Id = 6,
                Uid = specificId6,
                EventId = events[4].Id,
                Description = "",
                TicketCount = 14,
                Price = 200,
                Address = "Главный корпус ВГАУ, 3 этаж, спортзал",
                Date = new DateOnly(2024, 5, 4),
                DayOfWeek = DayOfWeek.Saturday,
                Time = new TimeOnly(19, 00, 00),
                Private = true,
                Code = "76c10355-6803-48d2-ab19-1ae99ba07f31".ToUpper().Replace("-", ""),
                Rating = 4
            },
        };

        this.specifics = specifics;
        await context.SpecificEvents.AddRangeAsync(specifics);
    }
}
