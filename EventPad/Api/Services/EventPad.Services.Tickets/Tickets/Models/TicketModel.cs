using AutoMapper;
using EventPad.Api.Context;
using EventPad.Api.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventPad.Api.Services.Tickets;

public class TicketModel
{
    public Guid Id { get; set; }
    public Guid SpecificId { get; set; }

    public TicketStatus Status { get; set; }
    public string FeedBack {  get; set; }
    public float Rating { get; set; }
}


public class TicketModelProfile : Profile
{
    public TicketModelProfile()
    {
        CreateMap<Ticket, TicketModel>()
            .BeforeMap<TicketModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SpecificId, opt => opt.Ignore())
            ;
    }
}

public class TicketModelActions : IMappingAction<Ticket, TicketModel>
{
    public readonly IDbContextFactory<ApiDbContext> dbContextFactory;

    public TicketModelActions(IDbContextFactory<ApiDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(Ticket source, TicketModel dest, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var model = db.Tickets.FirstOrDefault(x => x.Id == source.Id);

        dest.Id = model.Uid;
        dest.SpecificId = model.SpecificEvent.Uid;
    }
}