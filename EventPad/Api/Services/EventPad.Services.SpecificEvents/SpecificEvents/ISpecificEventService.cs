namespace EventPad.Api.Services.Specific;

public interface ISpecificEventService
{
    Task<IEnumerable<SpecificEventModel>> GetAllSpecificEvents(Guid userId, int page = 1, int pageSize = 10, SpecificEventModelFilter filter = null);
    Task<IEnumerable<SpecificEventModel>> GetCurrentSpecificEvents(Guid id, int page = 1, int pageSize = 10);
    Task<SpecificEventModel> GetById(Guid id);
    Task<SpecificEventModel> GetByCode(string code);
    Task<SpecificEventModel> Create(CreateSpecificModel model, Guid userId);
    Task<SpecificEventModel> Update(Guid id, UpdateSpecificEventModel model, Guid userId);
    Task Delete(Guid id, Guid userId);
}
