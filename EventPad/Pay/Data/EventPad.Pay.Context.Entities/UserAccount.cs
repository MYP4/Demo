namespace EventPad.Pay.Context.Entities;


public class UserAccount : BaseEntity
{
    public string AccountNumber { get; set; }
    public float Balance { get; set; }

    public virtual ICollection<Transaction>? Transactions { get; set; }
}
