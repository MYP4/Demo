using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Api.Services.Events;
using EventPad.Common;
using EventPad.Common.Files;

namespace EventPad.Api.Controllers.Events;

public class CreateEventRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string Address { get; set; }
    public EventType Type { get; set; }
    public FilePayload Image {  get; set; } 
}


public class EventCreateProfile : Profile
{
    public EventCreateProfile()
    {
        CreateMap<CreateEventRequest, CreateEventModel>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.ToFileData()));
        
    }
}


