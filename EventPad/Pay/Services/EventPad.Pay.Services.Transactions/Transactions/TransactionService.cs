using AutoMapper;
using EventPad.Common;
using EventPad.Pay.Context;
using EventPad.Pay.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Pay.Services.Transactions;

public class TransactionService : ITransactionService
{

    private readonly IDbContextFactory<PayDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateTransactionModel> createModelValidator;

    public TransactionService(IDbContextFactory<PayDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<CreateTransactionModel> createModelValidator)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createModelValidator = createModelValidator;
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
        await createModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();


        var transaction = mapper.Map<Transaction>(model);

        await context.Transactions.AddAsync(transaction);

        await context.SaveChangesAsync();

        return mapper.Map<TransactionModel>(transaction);
    }
}
