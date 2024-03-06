namespace EventPad.Api.Services.Specific;

public interface ISpecificEventService
{
    Task<IEnumerable<SpecificEventModel>> GetAllSpecificEvents(int page = 1, int pageSize = 10, SpecificEventModelFilter filter = null);
    Task<IEnumerable<SpecificEventModel>> GetCurrentSpecificEvents(Guid id, int page = 1, int pageSize = 10);
    Task<SpecificEventModel> GetById(Guid id);
    Task<SpecificEventModel> Create(CreateSpecificModel model);
    Task<SpecificEventModel> Update(Guid id, UpdateSpecificEventModel model);
    Task Delete(Guid id);
}
