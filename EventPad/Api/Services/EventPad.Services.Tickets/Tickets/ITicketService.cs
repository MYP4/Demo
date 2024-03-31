namespace EventPad.Api.Services.Tickets;

public interface ITicketService
{
    Task<IEnumerable<TicketModel>> GetAllTickets(int page = 1, int pageSize = 10, TicketModelFilter filter = null, string eventId = null, string userId = null);
    Task<TicketModel> GetById(Guid id, Guid userId);
    Task<TicketModel> Create(CreateTicketModel model);
    Task<TicketModel> Update(Guid id, UpdateTicketModel model, Guid userId);
    Task Delete(Guid id, Guid userId);
}
