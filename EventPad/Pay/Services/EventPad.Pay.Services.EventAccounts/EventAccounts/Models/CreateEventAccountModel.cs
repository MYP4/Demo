using FluentValidation;

namespace EventPad.Pay.Services.EventAccounts;

public class CreateEventAccountModel
{
    public Guid EventId { get; set; }
}

public class CreateEventAccountModelValidator : AbstractValidator<CreateEventAccountModel>
{
    public CreateEventAccountModelValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty().WithMessage("EventID is required");
    }
}