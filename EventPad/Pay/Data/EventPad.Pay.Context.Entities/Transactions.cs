namespace EventPad.Pay.Context.Entities;


public class Transaction : BaseEntity
{
    public TransactionType Type { get; set; }

    public int EventAccountId { get; set; }
    public virtual EventAccount EventAccount { get; set; }

    public int UserAccountId { get; set; }
    public virtual UserAccount UserAccount { get; set; }

    public string Details { get; set; }
    public Guid Ticket {  get; set; }
    public DateOnly Date {  get; set; }
    public TimeOnly Time { get; set; }
    public float Amount { get; set; }
}
