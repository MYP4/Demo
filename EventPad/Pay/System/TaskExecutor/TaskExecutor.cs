using EventPad.Logger;
using EventPad.Pay.Services.EventAccounts;
using EventPad.Pay.Services.UserAccounts;
using EventPad.RabbitMq;
using EventPad.Services.Actions;

namespace EventPad.Pay;

public class TaskExecutor : ITaskExecutor
{
    private readonly IAppLogger logger;
    private readonly IRabbitMq rabbitMq;
    private readonly IEventAccountService eventAccountService;
    private readonly IUserAccountService userAccountService;

    public TaskExecutor(
        IAppLogger logger,
        IRabbitMq rabbitMq
,
        IUserAccountService userAccountService,
        IEventAccountService eventAccountService)
    {
        this.logger = logger;
        this.rabbitMq = rabbitMq;
        this.userAccountService = userAccountService;
        this.eventAccountService = eventAccountService;
    }


    public void Start()
    {
        SubscribeOnCreateEventAccount();
        SubscribeOnCreateUserAccount();
        SubscribeOnDeleteEventAccount();
        SubscribeOnDeleteUserAccount();
    }

    private void SubscribeOnCreateEventAccount()
    {
        rabbitMq.Subscribe<CreateEventAccount>(QueueNames.CREATE_EVENT_ACCOUNT, async data =>
        {
            logger.Information($"Starting creating of the event_account::: {data.Id}");

            await eventAccountService.Create(new CreateEventAccountModel() { EventId = data.Id });

            logger.Information($"The event_account was created::: {data.Id}");
        });
    }

    private void SubscribeOnCreateUserAccount()
    {
        rabbitMq.Subscribe<CreateUserAccount>(QueueNames.CREATE_USER_ACCOUNT, async data =>
        {
            logger.Information($"Starting creating of the user_account::: {data.Id}");

            await userAccountService.Create(new CreateUserAccountModel() { UserId = data.Id });

            logger.Information($"The user_account was created::: {data.Id}");
        });
    }


    private void SubscribeOnDeleteEventAccount()
    {
        rabbitMq.Subscribe<Guid>(QueueNames.DELETE_EVENT_ACCOUNT, async data =>
        {
            logger.Information($"Starting removing of the event_account::: {data}");

            await eventAccountService.Delete(data);

            logger.Information($"The event_account removed::: {data}");
        });
    }

    private void SubscribeOnDeleteUserAccount()
    {
        rabbitMq.Subscribe<Guid>(QueueNames.DELETE_USER_ACCOUNT, async data =>
        {
            logger.Information($"Starting removing of the user_account::: {data}");

            await userAccountService.Delete(data);

            logger.Information($"The user_account removed::: {data}");
        });
    }


    private void SubscribeOnCreateTransaction()
    {
        throw new NotImplementedException();
    }
}
