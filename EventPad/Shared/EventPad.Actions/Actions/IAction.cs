using EventPad.Actions;
using EventPad.Actions.Actions.Models;

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

    Task GetUserAccount(GetUserAccountModel model);
}
