using EventPad.Actions;
using EventPad.Settings;

namespace EventPad.EmailService;



public class EmailSender : IEmailSender
{
    private readonly EmailSettings settings;

    public EmailSender(EmailSettings settings)
    {
        this.settings = settings;
    }

    public async Task SendEmail(SendEmailModel model)
    {
        await new SMTPProvider(settings).SendEmailAsync(model.Email, model.Subject, model.Message);
    }

    public async Task SendEmail(string email, string subject, string message)
    {
        await new SMTPProvider(settings).SendEmailAsync(email, subject, message);
    }
}
