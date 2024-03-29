using EventPad.Actions;

namespace EventPad.EmailService;


public interface IEmailSender
{
    Task SendEmail(SendEmailModel model);
    Task SendEmail(string email, string subject, string message);
}
