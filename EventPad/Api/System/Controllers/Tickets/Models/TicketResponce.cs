using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Api.Services.Tickets;

namespace EventPad.Api.Controllers.Tickets;

public class TicketResponce
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SpecificId { get; set; }
    public TicketStatus Status { get; set; }
    public string Feedback { get; set; }
    public float Rating { get; set; }
}

public class TicketResponceProfile : Profile
{
    public TicketResponceProfile()
    {
        CreateMap<TicketModel,  TicketResponce>();
    }
}
