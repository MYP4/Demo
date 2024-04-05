using AutoMapper;
using EventPad.Api.Services.Tickets;

namespace EventPad.Api.Controllers.Tickets;

public class CreateTicketRequest
{
    public Guid SpecificId { get; set; }
    public Guid UserId { get; set;}
}


public class TicketCreateProfile : Profile
{
    public TicketCreateProfile()
    {
        CreateMap<CreateTicketRequest, CreateTicketModel>();
    }
}