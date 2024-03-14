namespace EventPad.Actions.Actions;

public class CashoutTicket
{
    public Guid? UserAccountID { get; set; }
    public Guid? EventAccountID { get; set; }
    public float Amount { get; set; }
    public string Details { get; set; }
}
