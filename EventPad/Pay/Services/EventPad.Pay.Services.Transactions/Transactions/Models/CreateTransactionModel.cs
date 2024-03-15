using AutoMapper;
using EventPad.Pay.Context;
using EventPad.Pay.Context.Entities;

using Microsoft.EntityFrameworkCore;

namespace EventPad.Pay.Services.Transactions;

public class CreateTransactionModel
{
    public TransactionType Type { get; set; }

    public Guid EventAccountId { get; set; }
    public Guid UserAccountId { get; set; }
    public Guid TicketId { get; set; }

    public float Amount { get; set; }
    public string Details { get; set; }
}


public class CreateModelProfile : Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateTransactionModel, Transaction>()
            .ForMember(dest => dest.EventAccountId, opt => opt.Ignore())
            .ForMember(dest => dest.UserAccountId, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ForMember(dest => dest.Time, opt => opt.Ignore())
            .AfterMap<CreateModelActions>()
            ;
    }
}

public class CreateModelActions : IMappingAction<CreateTransactionModel, Transaction>
{
    private readonly IDbContextFactory<PayDbContext> dbContextFactory;

    public CreateModelActions(IDbContextFactory<PayDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(CreateTransactionModel source, Transaction dest, ResolutionContext context)
    {
         using var db = dbContextFactory.CreateDbContext();

        var eventAccount = db.EventAccounts.FirstOrDefault(x => x.Uid == source.EventAccountId);
        var userAccount = db.UserAccounts.FirstOrDefault(x => x.Uid == source.UserAccountId);

        dest.Uid = Guid.NewGuid();

        dest.Date = DateOnly.FromDateTime(DateTime.Now);
        dest.Time = TimeOnly.FromDateTime(DateTime.Now);

        dest.EventAccountId = eventAccount.Id;
        dest.UserAccountId = userAccount.Id;
    }
}

