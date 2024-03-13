using AutoMapper;
using EventPad.Pay.Context;
using EventPad.Pay.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Pay.Services.EventAccounts;

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
            ;
    }
}

public class EventAccountModelActions : IMappingAction<EventAccount, EventAccountModel>
{

    public readonly IDbContextFactory<PayDbContext> dbContextFactory;

    public EventAccountModelActions(IDbContextFactory<PayDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(EventAccount source, EventAccountModel dest, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var model = db.EventAccounts.FirstOrDefaultAsync(x => x.Uid == source.Uid).GetAwaiter().GetResult();

        dest.Id = model.Uid;
    }
}