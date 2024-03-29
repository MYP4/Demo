using EventPad.Actions;
using EventPad.Actions.Actions.Models;
using EventPad.Logger;
using EventPad.Pay.Context.Entities;
using EventPad.Pay.Services.EventAccounts;
using EventPad.Pay.Services.Transactions;
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
    private readonly ITransactionService transactionService;

    public TaskExecutor(
        IAppLogger logger,
        IRabbitMq rabbitMq
,
        IUserAccountService userAccountService,
        IEventAccountService eventAccountService,
        ITransactionService transactionService)
    {
        this.logger = logger;
        this.rabbitMq = rabbitMq;
        this.userAccountService = userAccountService;
        this.eventAccountService = eventAccountService;
        this.transactionService = transactionService;
    }


    public void Start()
    {
        SubscribeOnCreateEventAccount();
        SubscribeOnCreateUserAccount();
        SubscribeOnDeleteEventAccount();
        SubscribeOnDeleteUserAccount();

        SubscribeOnPayTransaction();
        SubscribeOnRefundTransaction();
    }

    private void SubscribeOnCreateEventAccount()
    {
        rabbitMq.Subscribe<CreateEventAccount>(QueueNames.CREATE_EVENT_ACCOUNT, async data =>
        {
            logger.Information($"Start creating of the event_account::: {data.Id}");

            await eventAccountService.Create(new CreateEventAccountModel() { EventId = data.Id });

            logger.Information($"The event_account was created::: {data.Id}");
        });
    }

    private void SubscribeOnCreateUserAccount()
    {
        rabbitMq.Subscribe<CreateUserAccount>(QueueNames.CREATE_USER_ACCOUNT, async data =>
        {
            logger.Information($"Start creating of the user_account::: {data.Id}");

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
            logger.Information($"Start removing of the user_account::: {data}");

            await userAccountService.Delete(data);

            logger.Information($"The user_account removed::: {data}");
        });
    }


    private void SubscribeOnPayTransaction()
    {
        rabbitMq.Subscribe<BuyTicket>(QueueNames.BUY_TICKET, async data =>
        {
            logger.Information($"Start purchase creating of the transaction::: {data}");

            await transactionService.Create(new CreateTransactionModel()
            {
                Type = TransactionType.Purchase,
                EventAccountId = data.EventAccountId,
                UserAccountId = data.UserAccountId,
                TicketId = data.Ticket,
                Amount = data.Amount,
            });

            logger.Information($"The purchase transaction created::: {data}");
        });
    }

    private void SubscribeOnRefundTransaction()
    {
        rabbitMq.Subscribe<RefundTicket>(QueueNames.REFUND_TICKET, async data =>
        {
            logger.Information($"Start creating of the refund transaction::: {data}");

            await transactionService.Create(new CreateTransactionModel()
            {
                Type = TransactionType.Refund,
                EventAccountId = data.EventAccountId,
                UserAccountId = data.UserAccountId,
                TicketId = data.Ticket,
                Amount = data.Amount,
            });

            logger.Information($"The refund transaction created::: {data}");
        });
    }
}
