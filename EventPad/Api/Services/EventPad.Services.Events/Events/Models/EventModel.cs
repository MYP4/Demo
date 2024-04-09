using AutoMapper;
using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using EventPad.Settings;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Api.Services.Events;

public class EventModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }
    public EventType Type { get; set; }
    public float Rating { get; set; }

    public Guid Account { get; set; }

    public Guid AdminId { get; set; }
    public string AdminName { get; set; }

    public string Image { get; set; }
}


public class EventModelProfile : Profile
{
    public EventModelProfile()
    {
        CreateMap<Event, EventModel>()
            .BeforeMap<EventModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.AdminId, opt => opt.Ignore())
            .ForMember(dest => dest.AdminName, opt => opt.Ignore())
            .ForMember(dest => dest.Image, opt => opt.Ignore())
            ;
    }
}


public class EventModelActions : IMappingAction<Event, EventModel>
{

    public readonly IDbContextFactory<ApiDbContext> dbContextFactory;
    private readonly MainSettings mainSettings;

    public EventModelActions(IDbContextFactory<ApiDbContext> dbContextFactory, MainSettings mainSettings)
    {
        this.dbContextFactory = dbContextFactory;
        this.mainSettings = mainSettings;
    }

    public void Process(Event source, EventModel dest, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var model = db.Events.FirstOrDefault(x => x.Id == source.Id);
        
        if (!string.IsNullOrEmpty(source.Image))
            dest.Image = Path.Combine(mainSettings.FileDir, source.Image);

        dest.Id = model.Uid;
        dest.AdminId = model.Admin.Id;
        dest.AdminName = model.Admin.FirstName + " " + model.Admin.SecondName;
    }
}