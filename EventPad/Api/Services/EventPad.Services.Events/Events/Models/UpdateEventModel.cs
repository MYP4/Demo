using AutoMapper;
using EventPad.Api.Context.Entities;
using FluentValidation;

namespace EventPad.Api.Services.Events;

public class UpdateEventModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }
    public EventType Type { get; set; }
}


public class UpdateModelProfile : Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateEventModel, Event>()
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            ;
    }
}

public class UpdateModelValidator : AbstractValidator<UpdateEventModel>
{
    public UpdateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(5).WithMessage("Minimal length is 5")
            .MaximumLength(100).WithMessage("Maxomal length is 100");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Maximum length is 1000");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Minimal price is 0");

        RuleFor(x => x.Address)
            .MaximumLength(1000).WithMessage("Maximum length is 1000");
    }
}