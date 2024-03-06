using AutoMapper;
using EventPad.Common.Exceptions;
using EventPad.Context;
using EventPad.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Services.EventAccounts;

public class EventAccountModel
{
    public Guid Id { get; set; }
    public string? AccountNumber { get; set; }
    public float Balance { get; set; }
}


public class EventAccountModelProfile : Profile
{
    public EventAccountModelProfile()
    {
        CreateMap<EventAccount, EventAccountModel>()
            .BeforeMap<EventAccountModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.AccountNumber, opt => opt.Ignore())
            ;
    }
}

public class EventAccountModelActions : IMappingAction<EventAccount, EventAccountModel>
{

    public readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public EventAccountModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(EventAccount source, EventAccountModel dest, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var model = db.Events.FirstOrDefaultAsync(x => x.Id == source.EventId).GetAwaiter().GetResult();

        dest.Id = model.Uid;
        dest.AccountNumber = source.AccountNumber;
    }
}