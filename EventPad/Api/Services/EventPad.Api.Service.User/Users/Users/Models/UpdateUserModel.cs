using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Common.Files;
using FluentValidation;

namespace EventPad.Api.Service.Users;

public class UpdateUserModel
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public FileData? Image { get; set; }
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
    }
}