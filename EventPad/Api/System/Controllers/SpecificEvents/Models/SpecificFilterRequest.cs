using AutoMapper;
using EventPad.Api.Services.Specific;

namespace EventPad.Api.Controllers.Specific;

public class SpecificFilterRequest
{
    public float? Price { get; set; }
    public string? Address { get; set; }
    public DateOnly? Date { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public TimeOnly? Time { get; set; }
    public bool? Private { get; set; }
}


public class SpecificFilterProfile : Profile
{
    public SpecificFilterProfile()
    {
        CreateMap<SpecificFilterRequest, SpecificEventModelFilter>();
    }
}