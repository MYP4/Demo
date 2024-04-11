using EventPad.Api.Context.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace EventPad.Web.Pages.Events;

public class CreateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }
    public EventType Type { get; set; }
    public IBrowserFile Image { get; set; }

    public Guid AdminId { get; set; }
}
