using AutoMapper;
using EventPad.Pay.Context;
using EventPad.Pay.Context.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Api.Services.EventAccounts;

public class CreateEventAccountModel
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; }
    public float Balance { get; set; }
}


public class CreateEventAccountModelProfile : Profile
{
    public CreateEventAccountModelProfile()
    {
        CreateMap<CreateEventAccountModel, EventAccount>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            //.AfterMap<CreateEventAccountModelActions>();
            ;
    }
}

//public class CreateEventAccountModelActions : IMappingAction<CreateEventAccountModel, EventAccount>
//{
//    private readonly IDbContextFactory<PayDbContext> dbContextFactory;

//    public CreateEventAccountModelActions(IDbContextFactory<PayDbContext> dbContextFactory)
//    {
//        this.dbContextFactory = dbContextFactory;
//    }

//    public void Process(CreateEventAccountModel source, EventAccount dest, ResolutionContext context)
//    {
//        using var db = dbContextFactory.CreateDbContext();

//        var eventAccount = db.EventAccounts.FirstOrDefault(x => x.Uid == source.Id);

//        dest. = eventAccount.Id;
//    }
//}

public class CreateEventAccountModelValidator : AbstractValidator<CreateEventAccountModel>
{
    public CreateEventAccountModelValidator(IDbContextFactory<PayDbContext> contextFactory)
    {
        RuleFor(x => x.AccountNumber)
            .NotEmpty().WithMessage("AccountNumber is required")
            .Length(16).WithMessage("AccountNumber length must be 16");


        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("Balance must be greater than or equal to  0");
    }
}