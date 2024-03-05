using AutoMapper;
using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Api.Services.Specific;

public class SpecificEventModel
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }

    public string Description { get; set; }
    public int TicketCount { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }

    public DateOnly? Date { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public TimeOnly Time { get; set; }

    public bool Private { get; set; }
    public string Code { get; set; }
    public float Ratings { get; set; }
}


public class SpecificModelProfile : Profile
{
    public SpecificModelProfile()
    {
        CreateMap<SpecificEvent, SpecificEventModel>()
            .BeforeMap<SpecificModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.EventId, opt => opt.Ignore())
            ;
    }
}

public class SpecificModelActions : IMappingAction<SpecificEvent, SpecificEventModel>
{
    public readonly IDbContextFactory<ApiDbContext> dbContextFactory;

    public SpecificModelActions(IDbContextFactory<ApiDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(SpecificEvent source, SpecificEventModel dest, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var model = db.SpecificEvents.FirstOrDefault(x => x.Id == source.Id);

        dest.Id = model.Uid;
        dest.EventId = model.Event.Uid;
    }
}