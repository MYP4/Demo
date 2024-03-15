using AutoMapper;
using EventPad.Api.Context.Entities;
using EventPad.Api.Services.Tickets;

namespace EventPad.Api.Controllers.Tickets;

public class TicketFilterRequest
{
    public Guid? EventId { get; set; }
    public Guid? UserId { get; set; }
    public TicketStatus? Status { get; set; }
}

public class SpecificFilterProfile : Profile
{
    public SpecificFilterProfile()
    {
        CreateMap<TicketFilterRequest, TicketModelFilter>();
    }
}