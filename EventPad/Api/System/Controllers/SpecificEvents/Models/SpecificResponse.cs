using AutoMapper;
using EventPad.Api.Services.Specific;

namespace EventPad.Api.Controllers.Specific;

public class SpecificResponse
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string Description { get; set; }
    public int TicketCount { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }
    public DateOnly? Date { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public TimeOnly? Time { get; set; }
    public bool Private { get; set; }
    public string Code { get; set; }
    public float Rating { get; set; }
}


public class SpecificResponceProfile : Profile
{
    public SpecificResponceProfile()
    {
        CreateMap<SpecificEventModel, SpecificResponse>();
    }
}
