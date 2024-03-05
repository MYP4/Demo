namespace EventPad.Api.Context.Entities;

public class EventPhoto : BaseEntity
{
    public int EventId { get; set; }
    public virtual Event Event { get; set; }

    public string Path { get; set; }
}
