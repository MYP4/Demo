using EventPad.Api.Context.Entities;

namespace EventPad.Web.Pages.Tickets;

public class TicketModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SpecificId { get; set; }
    public TicketStatus Status { get; set; }
    public string Feedback { get; set; }
    public float Rating { get; set; }
}
