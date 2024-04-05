namespace EventPad.Api.Services.Tickets;

public interface ITicketService
{
    Task<IEnumerable<TicketModel>> GetAllTickets(int page = 1, int pageSize = 10, TicketModelFilter filter = null);
    Task<IEnumerable<TicketModel>> GetUserTickets(Guid userId, int page = 1, int pageSize = 10);
    Task<IEnumerable<TicketModel>> GetSpecificTickets(Guid specificId, int page = 1, int pageSize = 10);
    Task<TicketModel> GetById(Guid id, Guid userId);
    Task<TicketModel> Create(CreateTicketModel model);
    Task<TicketModel> Update(Guid id, UpdateTicketModel model, Guid userId);
    Task Delete(Guid id, Guid userId);
}
