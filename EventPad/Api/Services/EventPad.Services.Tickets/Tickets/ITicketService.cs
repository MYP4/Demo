namespace EventPad.Api.Services.Tickets;

public interface ITicketService
{
    Task<IEnumerable<TicketModel>> GetAllTickets(int page = 1, int pageSize = 10, TicketModelFilter filter = null);
    Task<TicketModel> GetById(Guid id);
    Task<TicketModel> Create(CreateTicketModel model);
    Task<TicketModel> Update(Guid id, UpdateTicketModel model);
    Task Delete(Guid id);
}
