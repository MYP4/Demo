using EventPad.RabbitMq;

namespace EventPad.Api.Services.Actions;

public class Action : IAction
{
    private readonly IRabbitMq rabbitMq;

    public Action(IRabbitMq rabbitMq)
    {
        this.rabbitMq = rabbitMq;
    }

    public async Task CreateEventAccount(CreateEventAccount model)
    {
        await rabbitMq.PushAsync(QueueNames.CREATE_EVENT_ACCOUNT, model);
    }

    public async Task CreateUserAccount(CreateUserAccount model)
    {
        await rabbitMq.PushAsync(QueueNames.CREATE_USER_ACCOUNT, model);
    }
}

