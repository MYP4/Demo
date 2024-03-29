using EventPad.Actions;

namespace EventPad.Services.Actions;

public interface IAction
{
    Task CreateEventAccount(CreateEventAccount model);
    Task CreateUserAccount(CreateUserAccount model);
    Task DeleteEventAccount(Guid id);
    Task DeleteUserAccount(Guid id);

    Task BuyTicket(BuyTicket model);
    Task RefundTicket(RefundTicket model);


    Task SendEmail(SendEmailModel model);
}
