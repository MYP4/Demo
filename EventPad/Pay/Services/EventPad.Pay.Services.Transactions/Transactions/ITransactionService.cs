using EventPad.Pay.Context.Entities;

namespace EventPad.Pay.Services.Transactions;

public interface ITransactionService
{
    Task<IEnumerable<TransactionModel>> GetEventAccounts();
    Task<TransactionModel> GetEventAccountById(Guid id);
    Task<TransactionModel> Create(CreateTransactionModel model);
    Task<Transaction> Create();
    Task<TransactionModel> Update(Guid id, UpdateTransactionModel model);
    Task Delete(Guid id);
}
