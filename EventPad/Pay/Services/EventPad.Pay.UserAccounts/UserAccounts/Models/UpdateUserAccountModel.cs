using AutoMapper;
using EventPad.Pay.Context.Entities;
using FluentValidation;

namespace EventPad.Pay.Services.UserAccounts;

public class UpdateUserAccountModel
{
    public float Amount { get; set; }
}

public class UpdateUserAccountModelProfile : Profile
{
    public UpdateUserAccountModelProfile()
    {
        CreateMap<UpdateUserAccountModel, EventAccount>()
            .ForMember(dest => dest.AccountNumber, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            ;
    }
}

public class UpdateUserAccountModelValidator : AbstractValidator<UpdateUserAccountModel>
{
    public UpdateUserAccountModelValidator()
    {
        RuleFor(x => x.Amount)
           .NotEmpty().WithMessage("Amount is required");
    }
}
