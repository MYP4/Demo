using AutoMapper;
using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Api.Services.Tickets;

public class CreateTicketModel
{
    public Guid SpecificId { get; set; }
    public Guid UserId { get; set; }
}

public class CreateModelProfile : Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateTicketModel, Ticket>()
            .ForMember(dest => dest.SpecificEventId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Feedback, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.Ignore())
            .AfterMap<CreateModelActions>()
            ;
    }
}

public class CreateModelActions : IMappingAction<CreateTicketModel, Ticket>
{
    private readonly IDbContextFactory<ApiDbContext> dbContextFactory;

    public CreateModelActions(IDbContextFactory<ApiDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(CreateTicketModel sourse, Ticket dest, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var user = db.Users.FirstOrDefault(x => x.Id == sourse.UserId);
        var specific = db.SpecificEvents.FirstOrDefault(x => x.Uid == sourse.SpecificId);

        dest.UserId = user.Id;
        dest.SpecificEventId = specific.Id;
        dest.Status = TicketStatus.Paid;
    }
}

public class CreateModelValidator : AbstractValidator<CreateTicketModel>
{
    public CreateModelValidator(IDbContextFactory<ApiDbContext> contextFactory)
    {
        RuleFor(x => x.SpecificId)
            .NotEmpty().WithMessage("SpecificEvent is required")
            .Must((id) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.SpecificEvents.Any(a => a.Uid == id);
                return found;
            }).WithMessage("SpecificEvent not fount");
    }
}