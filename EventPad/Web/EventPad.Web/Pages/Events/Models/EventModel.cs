using EventPad.Api.Context.Entities;

namespace EventPad.Web.Pages.Events;

public class EventModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }
    public EventType Type { get; set; }
    public float Rating { get; set; }

    public Guid AdminId { get; set; }
    public string AdminName { get; set; }
}
