using AutoMapper;
using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Api.Services.Tickets;

public class UpdateTicketModel
{
    public TicketStatus Status { get; set; }
    public string? Feedback { get; set; }
    public float? Rating { get; set; }
}

public class UpdateModelProfile : Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateTicketModel, Ticket>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.SpecificEventId, opt => opt.Ignore())
            .ForMember(dest => dest.SpecificEvent, opt => opt.Ignore())
            ;
    }
}

public class UpdateModelValidator : AbstractValidator<UpdateTicketModel>
{
    public UpdateModelValidator(IDbContextFactory<ApiDbContext> contextFactory)
    {
        using var context = contextFactory.CreateDbContext();

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required");

        RuleFor(x => x.Feedback)
            .MaximumLength(5000).WithMessage("Maximum length is 5000");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5");
    }
}
