namespace EventPad.Web.Pages.SpecificEvents;

public class CreateSpecificModel
{
    public string? Description { get; set; }
    public int TicketCount { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }
    public DateOnly Date { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly Time { get; set; }
    public bool Private { get; set; }
}
