namespace EventPad.Api.Context.Entities;

public class Ticket : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public int SpecificEventId { get; set; }
    public virtual SpecificEvent SpecificEvent { get; set; }

    public TicketStatus Status { get; set; }
    public string Feedback { get; set; }
    public double Rating { get; set; }
}
