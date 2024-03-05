namespace EventPad.Api.Context.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public UserRole Role { get; set; }
    public double Ratings { get; set; }

    public Guid Account { get; set; }

    public virtual ICollection<Event> Events { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; }
}
