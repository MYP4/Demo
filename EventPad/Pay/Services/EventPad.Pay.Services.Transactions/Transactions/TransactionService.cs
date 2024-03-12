using EventPad.Pay.Context.Entities;

namespace EventPad.Pay.Services.Transactions;

public class TransactionService : ITransactionService
{
    public Task<TransactionModel> Create(CreateTransactionModel model)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> Create()
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionModel> GetEventAccountById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TransactionModel>> GetEventAccounts()
    {
        throw new NotImplementedException();
    }

    public Task<TransactionModel> Update(Guid id, UpdateTransactionModel model)
    {
        throw new NotImplementedException();
    }
}
