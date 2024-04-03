using EventPad.Api.Context.Entities;

namespace EventPad.Web.Pages.SpecificEvents;

public class UpdateModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float? Price { get; set; }
    public string? Address { get; set; }
    public EventType? Type { get; set; }
}
