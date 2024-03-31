namespace EventPad.EmailService;

using EventPad.Settings;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

public class SMTPProvider
{
    private readonly EmailSettings settings;

    public SMTPProvider(EmailSettings settings)
    {
        this.settings = settings;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(settings.FromName, settings.FromEmail));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = message;

        emailMessage.Body = bodyBuilder.ToMessageBody();
        using (var client = new SmtpClient())
        {
            try
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync(settings.Server, settings.Port, settings.Ssl);
                await client.AuthenticateAsync(settings.Login, settings.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }

}
