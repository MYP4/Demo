namespace EventPad.Web.Pages.SpecificEvents;

public interface ISpecificService
{
    Task<IEnumerable<SpecificModel>> GetSpecifics();
    Task<IEnumerable<SpecificModel>> GetEventSpecifics(Guid id);
    Task<SpecificModel> GetSpecific(Guid specificId);
    Task<SpecificModel> GetSpecificByCode(string code);
    Task AddSpecific(CreateSpecificModel model);
    Task EditSpecific(Guid specificId, UpdateModel model);
    Task DeleteSpecific(Guid specificId);
}
