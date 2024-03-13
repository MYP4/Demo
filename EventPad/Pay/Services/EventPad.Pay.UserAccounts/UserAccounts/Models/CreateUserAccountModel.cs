using FluentValidation;

namespace EventPad.Pay.Services.UserAccounts;

public class CreateUserAccountModel
{
    public Guid UserId { get; set; }
}


public class CreateUserAccountModelValidator : AbstractValidator<CreateUserAccountModel>
{
    public CreateUserAccountModelValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}