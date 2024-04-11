using Microsoft.AspNetCore.Identity;

namespace EventPad.Api.Context.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public UserRole Role { get; set; }
    public float Rating { get; set; }

    public Guid Account { get; set; }

    public string Image { get; set; }

    public virtual ICollection<Event> Events { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; }
}
