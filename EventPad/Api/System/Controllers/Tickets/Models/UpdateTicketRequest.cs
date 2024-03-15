using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Api.Services.Tickets;

namespace EventPad.Api.Controllers.Tickets;

public class UpdateTicketRequest
{
    public TicketStatus Status { get; set; }
    public string? Feedback { get; set; }
    public float? Rating { get; set; }
}


public class TicketUpdateProfile : Profile
{
    public TicketUpdateProfile()
    {
        CreateMap<UpdateTicketRequest, UpdateTicketModel>();
    }
}