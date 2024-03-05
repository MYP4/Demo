namespace EventPad.Api.Context.Entities;

public class SpecificEvent : BaseEntity
{
    public int EventId { get; set; }
    public virtual Event Event { get; set; }

    public string Description { get; set; }
    public int TicketCount { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }

    public DateOnly? Date { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public TimeOnly Time { get; set; }

    public bool Private { get; set; }
    public string Code { get; set; }
    public float Rating { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; }
}
