using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Api.Services.Events;

namespace EventPad.Api.Controllers.Events;

public class EventResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }
    public EventType Type { get; set; }
    public float Rating { get; set; }

    public Guid AdminId { get; set; }
    public string AdminName { get; set; }
}


public class EventResponceProfile : Profile
{
    public EventResponceProfile()
    {
        CreateMap<EventModel, EventResponse>();
    }
}