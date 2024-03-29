using EventPad.Actions;
using EventPad.EmailService;
using EventPad.Logger;
using EventPad.RabbitMq;
using EventPad.Services.Actions;


namespace EventPad.Worker;

public class TaskExecutor : ITaskExecutor
{
    private readonly IAppLogger logger;
    private readonly IRabbitMq rabbitMq;
    private readonly IEmailSender emailSender;


    public TaskExecutor(
        IAppLogger logger,
        IRabbitMq rabbitMq,
        IEmailSender emailSender)
    {
        this.logger = logger;
        this.rabbitMq = rabbitMq;
        this.emailSender = emailSender;
    }


    public void Start()
    {
        SubscribeOnSendEmail();
    }

    private void SubscribeOnSendEmail()
    {
        rabbitMq.Subscribe<SendEmailModel>(QueueNames.SEND_EMAIL, async data =>
        {
            logger.Information($"Start send email::: {data.Email}");

            await emailSender.SendEmail(data);

            logger.Information($"Email sent::: {data.Email}");
        });
    }

}
