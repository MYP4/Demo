using EventPad.Pay.Context.Entities;

namespace EventPad.Pay.Services.Transactions;

public class TransactionModelFilter
{
    public Guid? EventAccountId { get; set; }
    public Guid? UserAccountId { get; set; }
    public TransactionType? Type { get; set; }
    public DateOnly? Date {  get; set; }
}
