using EventPad.Api.Context.Entities;

namespace EventPad.Web.Pages.Tickets;

public class UpdateModel
{
    public TicketStatus Status { get; set; }
    public string? Feedback { get; set; }
    public float? Rating { get; set; }
}
