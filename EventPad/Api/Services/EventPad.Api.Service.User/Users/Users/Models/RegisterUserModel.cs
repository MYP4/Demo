using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Common.Files;
using FluentValidation;

namespace EventPad.Api.Service.Users;

public class RegiserUserModel
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public FileData Image { get; set; }
}

public class CreateModelProfile : Profile
{
    public CreateModelProfile()
    {
        CreateMap<RegiserUserModel, User>()
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            ;
    }
}

public class RegisterUserModelValidator : AbstractValidator<RegiserUserModel>
{
    public RegisterUserModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("User first name is required");

        RuleFor(x => x.SecondName)
            .NotEmpty().WithMessage("User second name is required");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(10).WithMessage("Password is short")
            .MaximumLength(50).WithMessage("Password is long");
    }
}
