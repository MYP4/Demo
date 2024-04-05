namespace EventPad.Web.Pages.Tickets;

public interface ITicketService
{
    Task<IEnumerable<TicketModel>> GetAllTickets(int page = 1, int pageSize = 10, TicketModelFilter filter = null);
    Task<IEnumerable<TicketModel>> GetUserTickets(int page = 1, int pageSize = 10);
    Task<IEnumerable<TicketModel>> GetSpecificTickets(Guid specificId, int page = 1, int pageSize = 10);
    Task<TicketModel> GetById(Guid id);
    Task AddTicket(CreateModel model);
    Task EditTicket(Guid id, UpdateModel model);
    Task DeleteTicket(Guid id);
}
