using AutoMapper;
using EventPad.Api.Services.Specific;

namespace EventPad.Api.Controllers.Specific;

public class CreateSpecificRequest
{
    public Guid EventId { get; set; }

    public string? Description { get; set; }
    public int TicketCount { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }
    public DateOnly? Date { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public TimeOnly Time { get; set; }
    public bool Private { get; set; }
}

public class SpecificCreateProfile : Profile
{
    public SpecificCreateProfile()
    {
        CreateMap<CreateSpecificRequest, CreateSpecificModel>();
    }
}