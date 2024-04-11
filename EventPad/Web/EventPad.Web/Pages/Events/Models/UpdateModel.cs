using EventPad.Api.Context.Entities;
using EventPad.Web.Common.Files;

namespace EventPad.Web.Pages.Events;

public class UpdateModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float? Price { get; set; }
    public string? Address { get; set; }
    public EventType? Type { get; set; }
    public FilePayload? Image { get; set; }
}
