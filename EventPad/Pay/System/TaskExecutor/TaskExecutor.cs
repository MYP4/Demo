using EventPad.Api.Services.Actions;
using EventPad.Logger;
using EventPad.Pay.Services.EventAccounts;
using EventPad.Pay.Services.UserAccounts;
using EventPad.RabbitMq;

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
        throw new NotImplementedException();
    }


    private void SubscribeOnDeleteEventAccount()
    {
        throw new NotImplementedException();
    }

    private void SubscribeOnDeleteUserAccount()
    {
        throw new NotImplementedException();
    }


    private void SubscribeOnCreatePurchase()
    {
        throw new NotImplementedException();
    }

    private void SubscribeOnCreateRefund()
    {
        throw new NotImplementedException();
    }

    private void SubscribeOnCreateCashout()
    {
        throw new NotImplementedException();
    }
}
