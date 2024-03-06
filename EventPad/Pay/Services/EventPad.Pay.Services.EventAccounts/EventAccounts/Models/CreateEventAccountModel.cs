using AutoMapper;
using EventPad.Context.Entities;
using EventPad.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Services.EventAccounts;

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
            .ForMember(dest => dest.EventId, opt => opt.Ignore())

            .AfterMap<CreateEventAccountModelActions>();
    }
}

public class CreateEventAccountModelActions : IMappingAction<CreateEventAccountModel, EventAccount>
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public CreateEventAccountModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(CreateEventAccountModel source, EventAccount dest, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var _event = db.Events.FirstOrDefault(e => e.Uid == source.Id);

        dest.EventId = _event.Id;
    }
}

public class CreateEventAccountModelValidator : AbstractValidator<CreateEventAccountModel>
{
    public CreateEventAccountModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {
        RuleFor(x => x.AccountNumber)
            .NotEmpty().WithMessage("AccountNumber is required")
            .Length(16).WithMessage("AccountNumber length must be 16");


        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("Balance must be greater than or equal to  0");
    }
}