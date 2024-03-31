namespace EventPad.Api.Context.Entities;

public class Event : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }
    public EventType Type {  get; set; }
    public float Rating { get; set; }

    public Guid AdminId { get; set; }
    public virtual User Admin { get; set; }

    public virtual ICollection<EventPhoto> Photos { get; set; }
    public virtual ICollection<SpecificEvent> SpecificEvents { get; set; }
}
