namespace EventPad.Pay.Services.Transactions;

public interface ITransactionService
{
    Task<IEnumerable<TransactionModel>> GetTransactions(int page = 1, int pageSize = 10, TransactionModelFilter filter = null);
    Task<TransactionModel> GetTransactionById(Guid id);
    Task<TransactionModel> Create(CreateTransactionModel model);
}
