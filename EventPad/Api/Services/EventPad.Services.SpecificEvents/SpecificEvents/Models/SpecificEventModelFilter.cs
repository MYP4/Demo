namespace EventPad.Api.Services.Specific;

public class SpecificEventModelFilter
{
    public float? Price { get; set; }
    public string? Address { get; set; }
    public DateOnly? Date { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public TimeOnly? Time { get; set; }
    public bool? Private { get; set; }
}
