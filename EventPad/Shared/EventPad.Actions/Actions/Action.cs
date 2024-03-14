using EventPad.RabbitMq;

namespace EventPad.Services.Actions;

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

    public async Task DeleteEventAccount(Guid id)
    {
        await rabbitMq.PushAsync(QueueNames.DELETE_EVENT_ACCOUNT, id);
    }

    public async Task DeleteUserAccount(Guid id)
    {
        await rabbitMq.PushAsync(QueueNames.DELETE_USER_ACCOUNT, id);
    }
}

