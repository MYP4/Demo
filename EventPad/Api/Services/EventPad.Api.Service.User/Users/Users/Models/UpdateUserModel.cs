using AutoMapper;
using EventPad.Api.Context.Entities;
using FluentValidation;

namespace EventPad.Api.Service.Users;

public class UpdateUserModel
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public float? Rating { get; set; }
    public string? Email { get; set; }
}

public class UpdateUserModelProfile : Profile
{
    public UpdateUserModelProfile()
    {
        CreateMap<UpdateUserModel, User>()
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            ;
    }
}

public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
{
    public UpdateUserModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("User first name is required");

        RuleFor(x => x.SecondName)
            .NotEmpty().WithMessage("User second name is required");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is required");
    }
}