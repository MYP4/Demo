namespace EventPad.Actions;

public class BuyTicket
{
    public Guid UserAccountId { get; set; }
    public Guid EventAccountId { get; set; }
    public Guid Ticket { get; set; }
    public float Amount { get; set; }
}
