namespace EventPad.Actions.Actions;

public class Cashout
{
    public Guid? AccountId { get; set; }
    public float Amount { get; set; }
    public string Details { get; set; }
}
