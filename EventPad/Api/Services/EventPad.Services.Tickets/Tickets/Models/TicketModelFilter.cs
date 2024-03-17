using EventPad.Api.Context.Entities;

namespace EventPad.Api.Services.Tickets;

public class TicketModelFilter
{
    public Guid? EventId { get; set; }
    public Guid? UserId { get; set; }
    public TicketStatus? Status { get; set; }
}
