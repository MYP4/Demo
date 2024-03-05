using AutoMapper;
using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Api.Services.Specific;

public class CreateSpecificEventModel
{
    public Guid EventId { get; set; }
    public string? Description { get; set; }
    public int TicketCount { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }

    public DateOnly? Date { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public TimeOnly Time { get; set; }

    public bool Private { get; set; }
}


public class CreateModelProfile : Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateSpecificEventModel, SpecificEvent>()
            .ForMember(dest => dest.EventId, opt => opt.Ignore())
            .ForMember(dest => dest.Code, opt => opt.Ignore())
            .ForMember(dest => dest.Tickets, opt => opt.Ignore())
            .AfterMap<CreateModelActions>()
            ;
    }
}

public class CreateModelActions : IMappingAction<CreateSpecificEventModel, SpecificEvent>
{
    private readonly IDbContextFactory<ApiDbContext> dbContextFactory;

    public CreateModelActions(IDbContextFactory<ApiDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(CreateSpecificEventModel sourse, SpecificEvent dest, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();
        var _event = db.Events.FirstOrDefault(x => x.Uid == sourse.EventId);

        var article = Guid.NewGuid().ToString().ToUpper().Replace("-", "");


        dest.EventId = _event.Id;
        dest.Code = article;
    }
}

public class CreateModelValidator : AbstractValidator<CreateSpecificEventModel>
{
    public CreateModelValidator(IDbContextFactory<ApiDbContext> contextFactory)
    {
        RuleFor(x => x.EventId)
            .NotEmpty().WithMessage("Event is required")
            .Must((id) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Events.Any(a => a.Uid == id);
                return found;
            }).WithMessage("Event not found");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Maximum length is 1000");

        RuleFor(x => x.TicketCount)
            .NotEmpty().WithMessage("TicketCount is required");

        RuleFor(x => x.Time)
            .NotEmpty().WithMessage("Time is required");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Minimal price is 0");

        RuleFor(x => x.Address)
            .MaximumLength(1000).WithMessage("Maximum length is 1000");

        RuleFor(x => x)
            .Must(model =>
            {
                using var context = contextFactory.CreateDbContext();
                var _event = context.Events.FirstOrDefault(a => a.Uid == model.EventId);
                if (_event.Type == EventType.Single)
                    return true;

                return model.DayOfWeek != null;
            })
            .WithName(nameof(CreateSpecificEventModel.DayOfWeek))
            .WithMessage("Day of week is required");

        RuleFor(x => x)
            .Must(model =>
            {
                using var context = contextFactory.CreateDbContext();
                var _event = context.Events.FirstOrDefault(a => a.Uid == model.EventId);
                if (_event.Type == EventType.Multiple)
                    return true;

                return model.Date != null;
            })
            .WithName(nameof(CreateSpecificEventModel.Date))
            .WithMessage("Date is required");
    }
}