using AutoMapper;
using EventPad.Context.Entities;
using FluentValidation;

namespace EventPad.Services.EventAccounts;

public class UpdateEventAccountModel
{
    public string AccountNumber { get; set; }
    public float Balance { get; set; }
}


public class UpdateEventAccountModelProfile : Profile
{
    public UpdateEventAccountModelProfile()
    {
        CreateMap<UpdateEventAccountModel, EventAccount>()
            .ForMember(dest => dest.AccountNumber, opt => opt.Ignore());
    }
}

public class UpdateEventAccountModelValidator : AbstractValidator<UpdateEventAccountModel>
{
    public UpdateEventAccountModelValidator()
    {
        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("Balance must be greater than or equal to  0");
    }
}