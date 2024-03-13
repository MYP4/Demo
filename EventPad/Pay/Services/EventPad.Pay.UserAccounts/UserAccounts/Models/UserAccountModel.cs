using AutoMapper;
using EventPad.Pay.Context;
using EventPad.Pay.Context.Entities;
using Microsoft.EntityFrameworkCore;


namespace EventPad.Pay.Services.UserAccounts;

public class UserAccountModel
{
    public Guid Id { get; set; }
    public string? AccountNumber { get; set; }
    public float Balance { get; set; }
}

public class UserAccountModelProfile : Profile
{
    public UserAccountModelProfile()
    {
        CreateMap<UserAccount, UserAccountModel>()
            .BeforeMap<UserAccountModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            ;
    }
}

public class UserAccountModelActions : IMappingAction<UserAccount, UserAccountModel>
{

    public readonly IDbContextFactory<PayDbContext> dbContextFactory;

    public UserAccountModelActions(IDbContextFactory<PayDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(UserAccount source, UserAccountModel dest, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var model = db.EventAccounts.FirstOrDefaultAsync(x => x.Uid == source.Uid).GetAwaiter().GetResult();

        dest.Id = model.Uid;
    }
}