using AutoMapper;
using EventPad.Pay.Context.Entities;
using FluentValidation;

namespace EventPad.Pay.Services.EventAccounts;

public class UpdateEventAccountModel
{
    public float Amount { get; set; }
}

public class UpdateEventAccountModelProfile : Profile
{
    public UpdateEventAccountModelProfile()
    {
        CreateMap<UpdateEventAccountModel, EventAccount>()
            .ForMember(dest => dest.AccountNumber, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            ;
    }
}

public class UpdateEventAccountModelValidator : AbstractValidator<UpdateEventAccountModel>
{
    public UpdateEventAccountModelValidator()
    {
        RuleFor(x => x.Amount)
           .NotEmpty().WithMessage("Amount is required");
    }
}