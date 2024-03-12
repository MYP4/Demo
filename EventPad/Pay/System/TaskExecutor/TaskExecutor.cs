using EventPad.Api.Services.Actions;
using EventPad.Logger;
using EventPad.RabbitMq;

namespace EventPad.Pay;

public class TaskExecutor : ITaskExecutor
{
    private readonly IAppLogger logger;
    private readonly IRabbitMq rabbitMq;

    public TaskExecutor(
        IAppLogger logger,
        IRabbitMq rabbitMq
    )
    {
        this.logger = logger;
        this.rabbitMq = rabbitMq;
    }


    public void CreateEventAccount()
    {
        rabbitMq.Subscribe<CreateEventAccount>(QueueNames.CREATE_EVENT_ACCOUNT, async data =>
        {
            logger.Information($"Starting creating of the event_account::: {data.Id}");

            await Task.Delay(3000);

            logger.Information($"The event_account was created::: {data.Id}");
        });
    }

    public void CreateUserAccount()
    {
        throw new NotImplementedException();
    }


    public void DeleteEventAccount()
    {
        throw new NotImplementedException();
    }

    public void DeleteUserAccount()
    {
        throw new NotImplementedException();
    }


    public void CreatePurchase()
    {
        throw new NotImplementedException();
    }

    public void CreateRefund()
    {
        throw new NotImplementedException();
    }

    public void CreateCashout()
    {
        throw new NotImplementedException();
    }
}
