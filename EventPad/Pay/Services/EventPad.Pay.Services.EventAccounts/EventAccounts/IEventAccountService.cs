namespace EventPad.Pay.Services.EventAccounts;

public interface IEventAccountService
{
    Task<IEnumerable<EventAccountModel>> GetEventAccounts();
    Task<EventAccountModel> GetEventAccountById(Guid id);
    Task<EventAccountModel> Create(CreateEventAccountModel model);
    Task<EventAccountModel> Update(Guid id, UpdateEventAccountModel model);
    Task Delete(Guid id);
}
