using AutoMapper;
using EventPad.Common;
using EventPad.Pay.Context;
using EventPad.Pay.Context.Entities;
using EventPad.Pay.Services.EventAccounts;
using EventPad.Pay.Services.UserAccounts;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EventPad.Pay.Services.Transactions;

public class TransactionService : ITransactionService
{

    private readonly IDbContextFactory<PayDbContext> dbContextFactory;
    private readonly IEventAccountService eventAccountService;
    private readonly IUserAccountService userAccountService;
    private readonly IMapper mapper;

    public TransactionService(
        IDbContextFactory<PayDbContext> dbContextFactory,
        IEventAccountService eventAccountService,
        IUserAccountService userAccountService,
        IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.eventAccountService = eventAccountService;
        this.userAccountService = userAccountService;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<TransactionModel>> GetTransactions(int page = 1, int pageSize = 10, TransactionModelFilter filter = null)
    {
        var eventAccount = filter?.EventAccountId;
        var userAccount = filter?.UserAccountId;
        var type = filter?.Type;
        var date = filter?.Date;

        using var context = await dbContextFactory.CreateDbContextAsync();

        var transactions = context.Transactions.AsQueryable();

        if (eventAccount != null)
        {
            transactions = transactions.Where(e => e.EventAccount.Uid == eventAccount);
        }

        if (userAccount != null)
        {
            transactions = transactions.Where(e => e.UserAccount.Uid == userAccount);
        }

        if (type != null)
        {
            transactions = transactions.Where(e => e.Type == type);
        }

        if (date != null)
        {
            transactions = transactions.Where(e => e.Date == date);
        }

        var transactionList = await transactions.ToListAsync();

        var result = mapper.Map<IEnumerable<TransactionModel>>(transactionList);

        return result;
    }

    public async Task<TransactionModel> GetTransactionById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var transaction = context.Transactions.FirstOrDefaultAsync(x => x.Uid == id);

        if (transaction == null)
            throw new ProcessException($"Transaction (ID = {id}) not found.");

        var result = mapper.Map<TransactionModel>(transaction);

        return result;
    }

    public async Task<TransactionModel> Create(CreateTransactionModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var transaction = mapper.Map<Transaction>(model);
        var userAccount = await context.UserAccounts.FirstOrDefaultAsync(x => x.Uid == model.UserAccountId);
        var eventAccount = await context.EventAccounts.FirstOrDefaultAsync(x => x.Uid == model.EventAccountId);

        var type = transaction.Type;

        await context.Transactions.AddAsync(transaction);

        if (type == TransactionType.Purchase)
        {
            try
            {
                if (userAccount.Balance <= model.Amount)
                    throw new Exception();

                await userAccountService.Update(userAccount.Uid, new UpdateUserAccountModel() { Amount = -transaction.Amount }, context);
                await eventAccountService.Update(eventAccount.Uid, new UpdateEventAccountModel() { Amount = transaction.Amount }, context);
            }
            catch (Exception ex)
            {
                throw new ProcessException("Недостаточно средств");
            }
        }
        if (type == TransactionType.Refund)
        {
            await eventAccountService.Update(eventAccount.Uid, new UpdateEventAccountModel() { Amount = -transaction.Amount }, context);
            await userAccountService.Update(userAccount.Uid, new UpdateUserAccountModel() { Amount = transaction.Amount }, context);
        }
        if (type == TransactionType.Cashout)
        {
            // Здесь можно подключить внешний сервис
        }
        if (type == TransactionType.Add)
        {
            await userAccountService.Update(userAccount.Uid, new UpdateUserAccountModel() { Amount = 1000 }, context);
        }

        await context.SaveChangesAsync();

        return mapper.Map<TransactionModel>(transaction);
    }
}
