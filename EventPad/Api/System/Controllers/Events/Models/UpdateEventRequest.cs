﻿using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Api.Services.Events;

namespace EventPad.Api.Controllers.Events;

public class UpdateEventRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float? Price { get; set; }
    public string? Address { get; set; }
    public EventType? Type { get; set; }
}


public class EventUpdateProfile : Profile
{
    public EventUpdateProfile()
    {
        CreateMap<UpdateEventRequest, UpdateEventModel>();
    }
}

